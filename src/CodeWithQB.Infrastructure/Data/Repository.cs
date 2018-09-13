using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using CodeWithQB.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CodeWithQB.Infrastructure.Data
{
    public class Repository : IRepository
    {
        private ConcurrentDictionary<string, ConcurrentBag<AggregateRoot>> _aggregates { get; set; } 
            = new ConcurrentDictionary<string, ConcurrentBag<AggregateRoot>>();

        public Repository(IEventStore eventStore)
        {            
            foreach (var item in eventStore.GetStateAsync().GetAwaiter().GetResult())
                _aggregates.TryAdd(item.Key, new ConcurrentBag<AggregateRoot>(item.Value.Select(x => (AggregateRoot)x).ToList()));

            eventStore.Subscribe(OnNext);
        }
        
        private void OnNext(EventStoreChanged value)
        {
            var id = $"{value.Event.Aggregate}Id";
            var aggregates = _aggregates.Single(x => x.Key == value.Event.DotNetType).Value;

            foreach (var aggregate in aggregates)
            {
                var type = aggregate.GetType();
                Guid aggregateId = (Guid)type.GetProperty($"{type.Name}Id").GetValue(aggregate, null);

                if (value.Event.StreamId == aggregateId) {
                    aggregate.Apply(JsonConvert.DeserializeObject<DomainEvent>(value.Event.Data));

                    var newAggregates = new ConcurrentBag<AggregateRoot>() { aggregate };

                    foreach (var originalAggregate in aggregates)
                    {
                        var originalId = (Guid)type.GetProperty($"{type.Name}Id").GetValue(originalAggregate, null);

                        if (aggregateId != originalId)
                            newAggregates.Add(originalAggregate);
                    }

                    _aggregates.TryUpdate(type.AssemblyQualifiedName, newAggregates, aggregates);
                }
            }            
        }

        public TAggregateRoot[] Query<TAggregateRoot>() where TAggregateRoot : AggregateRoot
        {
            var result = new List<TAggregateRoot>();
            var assemblyQualifiedName = typeof(TAggregateRoot).AssemblyQualifiedName;

            _aggregates.TryGetValue(assemblyQualifiedName, out ConcurrentBag<AggregateRoot> aggregates);

            foreach (var a in aggregates)
                result.Add(a as TAggregateRoot);

            return result.ToArray();
        }

        public TAggregateRoot Query<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot
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
