using CodeWithQB.Core.Common;
using CodeWithQB.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace UnitTests
{

    public class MockEventStore : IEventStore
    {
        private readonly Subject<EventStoreChanged> _subject = new Subject<EventStoreChanged>();
        private readonly Dictionary<string, IEnumerable<object>> _state = new Dictionary<string, IEnumerable<object>>();

        public MockEventStore(Dictionary<string, IEnumerable<object>> state)
        {
            _state = state;
        }
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        

        public async Task<Dictionary<string, IEnumerable<object>>> GetStateAsync()
        {
            return await Task.FromResult(_state);
        }

        public Task PersistStateAsync()
        {
            throw new NotImplementedException();
        }

        public TAggregateRoot[] Query<TAggregateRoot>() where TAggregateRoot : AggregateRoot
        {
            throw new NotImplementedException();
        }

        public void Save(AggregateRoot aggregateRoot)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(Action<EventStoreChanged> onNext)
        {
            _subject.Subscribe(onNext);
        }

        public ConcurrentDictionary<string, ConcurrentBag<AggregateRoot>> UpdateState<TAggregateRoot>(Type type, TAggregateRoot aggregateRoot, Guid aggregateId) where TAggregateRoot : AggregateRoot
        {
            throw new NotImplementedException();
        }
    }
}
