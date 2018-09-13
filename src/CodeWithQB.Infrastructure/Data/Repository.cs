using CodeWithQB.Core.Common;
using CodeWithQB.Core.Interfaces;
using System;
using System.Collections.Concurrent;

namespace CodeWithQB.Infrastructure.Data
{
    public class Repository : IRepository
    {
        public ConcurrentDictionary<string, ConcurrentBag<AggregateRoot>> Aggregates { get; set; } 
            = new ConcurrentDictionary<string, ConcurrentBag<AggregateRoot>>();

        public Repository(IEventStore eventStore)
        {
            eventStore.Subscribe(OnNext);
        }
        
        private void OnNext(EventStoreChanged value)
        {
            Console.WriteLine("Works?");
        }

        public TAggregateRoot[] Query<TAggregateRoot>() where TAggregateRoot : AggregateRoot
        {
            throw new System.NotImplementedException();
        }

        public TAggregateRoot Query<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot
        {
            throw new System.NotImplementedException();
        }
    }
}
