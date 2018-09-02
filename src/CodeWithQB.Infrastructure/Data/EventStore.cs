using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using static CodeWithQB.Infrastructure.Data.DeserializedEventStore;
using static Newtonsoft.Json.JsonConvert;

namespace CodeWithQB.Infrastructure.Data
{

    public class EventStore : IEventStore
    {
        private readonly IDateTime _dateTime;
        private readonly IBackgroundTaskQueue _queue;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EventStore(
            IDateTime dateTime = default(IDateTime),
            IBackgroundTaskQueue queue = default(IBackgroundTaskQueue),
            IServiceScopeFactory serviceScopeFactory = default(IServiceScopeFactory)
            )
        {
            _queue = queue;
            _serviceScopeFactory = serviceScopeFactory;
            _dateTime = dateTime;
            OnActivateAsync().GetAwaiter().GetResult();
        }

        public ConcurrentDictionary<string, ConcurrentBag<AggregateRoot>> Aggregates { get; set; }
        = new ConcurrentDictionary<string, ConcurrentBag<AggregateRoot>>();

        private DateTime? _asOfDate { get; set; }

        public async Task OnActivateAsync() {

            Dictionary<string,IEnumerable<object>> state = await LoadStateAsync();

            if (state == null)
            {
                var types = new List<Type>() {
                    typeof(Address),
                    typeof(Card),
                    typeof(Dashboard),
                    typeof(DashboardCard),
                    typeof(Event),
                    typeof(Guest),
                    typeof(Mentee),
                    typeof(NotificationTemplate),
                    typeof(Product),
                    typeof(Role),
                    typeof(User)
                };

                foreach (var type in types)
                    Aggregates.TryAdd(type.AssemblyQualifiedName, new ConcurrentBag<AggregateRoot>(Query(type.AssemblyQualifiedName)));
            }
            else
            {
                foreach (var item in state)
                {
                    Aggregates.TryAdd(item.Key, new ConcurrentBag<AggregateRoot>(item.Value.Select(x => (AggregateRoot)x).ToList()));
                }             
            }

            await Task.CompletedTask;
        }

        public async Task<Dictionary<string, IEnumerable<object>>> LoadStateAsync() {
            using (var scope = _serviceScopeFactory.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
            {
                var snapshot = context.Snapshots.OrderByDescending(x => x.AsOfDateTime).FirstOrDefault();

                if (snapshot == null) return null;

                var result = new Dictionary<string, IEnumerable<object>>();

                foreach (var item in DeserializeObject<dynamic>(snapshot.Data))
                {
                    var aggregates = new List<object>();
                    foreach (var dataItem in ((JProperty)item).Value.ToList())
                    {
                        aggregates.Add(DeserializeObject(SerializeObject(dataItem), Type.GetType(((JProperty)item).Name)));
                    }

                    result.TryAdd(((JProperty)item).Name, aggregates);
                }

                return await Task.FromResult(result);
            }
        }

        public async Task PersistStateAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
            {
                Dictionary<string, IEnumerable<AggregateRoot>> data = new Dictionary<string, IEnumerable<AggregateRoot>>();

                foreach (var item in Aggregates)
                    data.Add(item.Key, item.Value);

                context.Snapshots.Add(new Snapshot()
                {
                    AsOfDateTime = _dateTime.UtcNow,
                    Data = SerializeObject(data),
                });

                await context.SaveChangesAsync(default(CancellationToken));
            }
        }

        public void Dispose() {
            using (var scope = _serviceScopeFactory.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
            {
                PersistStateAsync().GetAwaiter().GetResult();
                context.Dispose();
            }
        }

        public void Save(AggregateRoot aggregateRoot)
        {
            
            var type = aggregateRoot.GetType();
            Guid aggregateId = (Guid)type.GetProperty($"{type.Name}Id").GetValue(aggregateRoot, null);
            string aggregate = aggregateRoot.GetType().Name;
            
            foreach (var @event in aggregateRoot.DomainEvents)
            {
                Add(new StoredEvent()
                {
                    StoredEventId = Guid.NewGuid(),
                    Aggregate = aggregate,
                    Data = SerializeObject(@event),
                    StreamId = aggregateId,
                    DotNetType = @event.GetType().AssemblyQualifiedName,
                    Type = @event.GetType().Name,
                    CreatedOn = DateTime.UtcNow,
                    Sequence = Get().Count() + 1
                });
            }

            aggregateRoot.ClearEvents();

            Aggregates.TryGetValue(type.AssemblyQualifiedName, out ConcurrentBag<AggregateRoot> concurrentBag);

            if (concurrentBag == null)
                concurrentBag = new ConcurrentBag<AggregateRoot>();

            concurrentBag.Add(aggregateRoot);
           
            Aggregates.AddOrUpdate(type.AssemblyQualifiedName, concurrentBag, (key, oldValue) => concurrentBag);
        }

        public T Query<T>(Guid id)
            where T : AggregateRoot
        {
            var list = new List<DomainEvent>();

            foreach (var storedEvent in Get().Where(x => x.StreamId == id))
                list.Add(storedEvent.Data as DomainEvent);

            return Load<T>(list.ToArray());
        }

        private T Load<T>(DomainEvent[] events)
            where T : AggregateRoot
        {
            var aggregate = (T)FormatterServices.GetUninitializedObject(typeof(T));

            foreach (var @event in events) aggregate.Apply(@event);

            aggregate.ClearEvents();

            return aggregate;
        }

        private AggregateRoot Load(Type type, DomainEvent[] events)
        {            
            var aggregate = (AggregateRoot)FormatterServices.GetUninitializedObject(type);

            foreach (var @event in events) aggregate.Apply(@event);

            aggregate.ClearEvents();

            return aggregate;
        }


        public TAggregateRoot Query<TAggregateRoot>(string propertyName, string value)
            where TAggregateRoot : AggregateRoot
        {
            var storedEvents = Get()
                .Where(x => {
                    var prop = Type.GetType(x.DotNetType).GetProperty(propertyName);
                    return prop != null && $"{prop.GetValue(x.Data, null)}" == value;
                })
                .ToArray();

            if (storedEvents.Length < 1) return null;

            return Query<TAggregateRoot>(storedEvents.First().StreamId) as TAggregateRoot;
        }

        public TAggregateRoot[] Query<TAggregateRoot>()
            where TAggregateRoot : AggregateRoot
        {
            var aggregates = new List<TAggregateRoot>();

            foreach (var grouping in Get()
                .Where(x => x.Aggregate == typeof(TAggregateRoot).Name).GroupBy(x => x.StreamId))
            {
                var events = grouping.Select(x => x.Data as DomainEvent).ToArray();

                aggregates.Add(Load<TAggregateRoot>(events.ToArray()));
            }

            return aggregates.ToArray();
        }

        public AggregateRoot[] Query(string dotNetType)
        {
            var type = Type.GetType(dotNetType);

            var aggregates = new List<AggregateRoot>();

            foreach (var grouping in Get()
                .Where(x => x.Aggregate == type.Name).GroupBy(x => x.StreamId))
            {
                var events = grouping.Select(x => x.Data as DomainEvent).ToArray();

                aggregates.Add(Load(type, events.ToArray()));
            }

            return aggregates.ToArray();
        }


        protected List<DeserializedStoredEvent> Get()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
            {
                if (Events == null)
                    Events = new ConcurrentDictionary<Guid, DeserializedStoredEvent>(context.StoredEvents.Select(x => new DeserializedStoredEvent(x)).ToDictionary(x => x.StoredEventId));

                return Events.Select(x => x.Value)
                    .OrderBy(x => x.CreatedOn)
                    .ToList();
            }
        }

        protected void Add(StoredEvent @event)
        {
            Events.TryAdd(@event.StoredEventId, new DeserializedStoredEvent(@event));
            Persist(@event);
        }

        public void Persist(StoredEvent @event)
        {
            if (_queue == null) {
                using (var scope = _serviceScopeFactory.CreateScope())
                using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    context.StoredEvents.Add(@event);
                    context.SaveChanges();
                }
            }

            _queue?.QueueBackgroundWorkItem(async token => await PersistStateAsync());
            
            _queue?.QueueBackgroundWorkItem(async token =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    context.StoredEvents.Add(@event);
                    context.SaveChanges();
                }

                await Task.CompletedTask;
            });

        }
    }
}
