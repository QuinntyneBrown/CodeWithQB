using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using CodeWithQB.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CodeWithQB.Infrastructure.Data
{
    public class Repository : IRepository
    {
        private ConcurrentDictionary<string, ConcurrentBag<Entity>> _aggregates { get; set; } 
            = new ConcurrentDictionary<string, ConcurrentBag<Entity>>();

        public Repository(IEventStore eventStore)
        {
            var storedEvents = eventStore.GetStoredEvents().GetAwaiter().GetResult();

            if(storedEvents != null)
                foreach (var @event in storedEvents)
                    OnNext(new EventStoreChanged(@event));
            
            eventStore.Subscribe(OnNext);
        }
        
        public void OnNext(EventStoreChanged value)
        {
            var type = Type.GetType(value.Event.AggregateDotNetType);
            var aggregates = _aggregates.SingleOrDefault(x => x.Key == value.Event.AggregateDotNetType).Value;            
            var aggregateId = value.Event.StreamId;

            if (aggregates != null)
            {
                Entity e = default(Entity);
                foreach (var aggregate in aggregates)
                {                                        
                    if (value.Event.StreamId == (Guid)type.GetProperty($"{type.Name}Id").GetValue(aggregate, null))
                        e = aggregate;
                }

                if (e == default(Entity))
                    e = (Entity)FormatterServices.GetUninitializedObject(Type.GetType(value.Event.AggregateDotNetType));

                
                e.Apply(JsonConvert.DeserializeObject(value.Event.Data,Type.GetType(value.Event.DotNetType)) as DomainEvent);

                e.ClearChanges();

                var newAggregates = new ConcurrentBag<Entity>() { e };

                foreach (var originalAggregate in aggregates)
                {
                    var originalId = (Guid)type.GetProperty($"{type.Name}Id").GetValue(originalAggregate, null);

                    if (aggregateId != originalId)
                        newAggregates.Add(originalAggregate);
                }

                _aggregates.TryUpdate(type.AssemblyQualifiedName, newAggregates, aggregates);
            }
            
            if (aggregates == null)
            {
                var aggregate = (Entity)FormatterServices.GetUninitializedObject(Type.GetType(value.Event.AggregateDotNetType));

                aggregates = new ConcurrentBag<Entity>() { aggregate };

                var domainEvent = JsonConvert.DeserializeObject(value.Event.Data, Type.GetType(value.Event.DotNetType)) as DomainEvent;

                aggregate.Apply(domainEvent);

                aggregate.ClearChanges();
                
                _aggregates.TryAdd(value.Event.AggregateDotNetType, aggregates);

            }            
        }

        public TAggregateRoot[] Query<TAggregateRoot>() where TAggregateRoot : Entity
        {
            var result = new List<TAggregateRoot>();
            var assemblyQualifiedName = typeof(TAggregateRoot).AssemblyQualifiedName;

            _aggregates.TryGetValue(assemblyQualifiedName, out ConcurrentBag<Entity> aggregates);

            if(aggregates != null)
                foreach (var a in aggregates)
                    result.Add(a as TAggregateRoot);

            return result.ToArray();
        }

        public TAggregateRoot[] Query<TAggregateRoot>(IEnumerable<Guid> ids) where TAggregateRoot : Entity
        {
            var type = typeof(TAggregateRoot);
            var result = new List<TAggregateRoot>();
            var assemblyQualifiedName = typeof(TAggregateRoot).AssemblyQualifiedName;

            _aggregates.TryGetValue(assemblyQualifiedName, out ConcurrentBag<Entity> aggregates);

            if (aggregates == null) return result.ToArray();

            foreach (var aggregate in aggregates)
            {
                if (ids.Contains((Guid)type.GetProperty($"{type.Name}Id").GetValue(aggregate, null)))
                    result.Add(aggregate as TAggregateRoot);                
            }
            
            return result.ToArray();
        }

        public TAggregateRoot Query<TAggregateRoot>(Guid id) where TAggregateRoot : Entity
        {
            var type = typeof(TAggregateRoot);
            var result = default(TAggregateRoot);

            foreach(var aggregate in Query<TAggregateRoot>())
            {
                if (id == (Guid)type.GetProperty($"{type.Name}Id").GetValue(aggregate, null))
                    result = aggregate;
            }

            return result;
        }
    }
}
