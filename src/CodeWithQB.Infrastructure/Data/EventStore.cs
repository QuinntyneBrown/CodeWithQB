using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using static CodeWithQB.Infrastructure.Data.DeserializedEventStore;
using static Newtonsoft.Json.JsonConvert;

namespace CodeWithQB.Infrastructure.Data
{

    public class EventStore : IEventStore
    {
        private readonly IAppDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly IMediator _mediator;
        private readonly IBackgroundTaskQueue _queue;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public EventStore(
            IAppDbContext context,
            IDateTime dateTime = default(IDateTime),
            IMediator mediator = default(IMediator),
            IBackgroundTaskQueue queue = default(IBackgroundTaskQueue),
            IServiceScopeFactory serviceScopeFactory = default(IServiceScopeFactory)
            )
        {
            _context = context;
            _mediator = mediator;
            _queue = queue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Dispose() => _context.Dispose();

        public void Save(AggregateRoot aggregateRoot)
        {
            var type = aggregateRoot.GetType();

            foreach (var @event in aggregateRoot.DomainEvents)
            {
                Add(new StoredEvent()
                {
                    StoredEventId = Guid.NewGuid(),
                    Aggregate = aggregateRoot.GetType().Name,
                    Data = SerializeObject(@event),
                    StreamId = (Guid)type.GetProperty($"{type.Name}Id").GetValue(aggregateRoot, null),
                    DotNetType = @event.GetType().AssemblyQualifiedName,
                    Type = @event.GetType().Name,
                    CreatedOn = DateTime.UtcNow
                });

                if (_mediator != null) _mediator.Publish(@event).GetAwaiter().GetResult();
            }

            aggregateRoot.ClearEvents();
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

        protected List<DeserializedStoredEvent> Get()
        {
            if (Events == null)
                Events = new ConcurrentDictionary<Guid, DeserializedStoredEvent>(_context.StoredEvents.Select(x => new DeserializedStoredEvent(x)).ToDictionary(x => x.StoredEventId));

            return Events.Select(x => x.Value)
                .OrderBy(x => x.CreatedOn)
                .ToList();
        }

        protected void Add(StoredEvent @event)
        {
            Events.TryAdd(@event.StoredEventId, new DeserializedStoredEvent(@event));
            Persist(@event);
        }

        public void Persist(StoredEvent @event)
        {
            if (_queue == null) _context.StoredEvents.Add(@event);

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
