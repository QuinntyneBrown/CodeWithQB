using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;

namespace CodeWithQB.Infrastructure.Data
{
    public class EventStore : IEventStore
    {
        private readonly IConfiguration _configuration;
        private readonly IDateTime _dateTime;
        private readonly IBackgroundTaskQueue _queue;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public Subject<EventStoreChanged> _subject = new Subject<EventStoreChanged>();

        public static ConcurrentDictionary<Guid, DeserializedStoredEvent> Events { get; set; }        

        public EventStore(
            IConfiguration configuration,
            IDateTime dateTime = default(IDateTime),
            IBackgroundTaskQueue queue = default(IBackgroundTaskQueue),
            IServiceScopeFactory serviceScopeFactory = default(IServiceScopeFactory)
            )
        {
            _configuration = configuration;
            _queue = queue;
            _serviceScopeFactory = serviceScopeFactory;
            _dateTime = dateTime;
        }

        public async Task<IEnumerable<StoredEvent>> GetEvents() {
            var storedEvents = default(IEnumerable<StoredEvent>);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                storedEvents = context.StoredEvents.OrderBy(x => x.Sequence).ToList();
            }

            return await Task.FromResult(storedEvents);
        }
        

        public void Save(Entity aggregateRoot)
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
                    AggregateDotNetType = type.AssemblyQualifiedName,
                    Data = SerializeObject(@event),
                    StreamId = aggregateId,
                    DotNetType = @event.GetType().AssemblyQualifiedName,
                    Type = @event.GetType().Name,
                    CreatedOn = DateTime.UtcNow,
                    Sequence = Get().Count() + 1
                });
            }
            aggregateRoot.ClearChanges();
        }
        

        public TAggregateRoot Load<TAggregateRoot>(Guid id)
            where TAggregateRoot : Entity
        {
            var events = Get().Where(x => x.StreamId == id);

            if (events.Count() == 0) return null;

            var aggregate = (Entity)FormatterServices.GetUninitializedObject(Type.GetType(typeof(TAggregateRoot).AssemblyQualifiedName));

            foreach(var @event in events)
                aggregate.Apply(@event.Data as DomainEvent);

            aggregate.ClearChanges();

            return aggregate as TAggregateRoot;
        }
        
        public List<DeserializedStoredEvent> Get()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

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

            _subject.OnNext(new EventStoreChanged(@event));

            _queue?.QueueBackgroundWorkItem(async token =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    context.StoredEvents.Add(@event);
                    await context.SaveChangesAsync(token);
                }
            });
        }
        
        public void Subscribe(Action<EventStoreChanged> onNext) => _subject.Subscribe(onNext);        
    }
}
