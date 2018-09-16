using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using CodeWithQB.Core.Models;
using CodeWithQB.Infrastructure.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class RepositoryTests
    {
        private Repository _repository;
        public RepositoryTests()
        {
            
        }

        [Fact]
        public void CanCreateRepository() {

            _repository = new Repository(new MockEventStore(null));
        }

        [Fact]
        public void CanHandleEventStoreChanged()
        {

            _repository = new Repository(new MockEventStore(new Dictionary<string, IEnumerable<object>>()));

            
            var type = typeof(User);
            Guid user1Id = Guid.NewGuid();
            var @event1 = new UserCreated(user1Id, "User1", null, "password");

            _repository.OnNext(new EventStoreChanged(new StoredEvent()
            {
                StoredEventId = Guid.NewGuid(),
                Aggregate = type.Name,
                AggregateDotNetType = type.AssemblyQualifiedName,
                Data = JsonConvert.SerializeObject(@event1),
                StreamId = user1Id,
                DotNetType = @event1.GetType().AssemblyQualifiedName,
                Type = @event1.GetType().Name,
                CreatedOn = DateTime.UtcNow,
                Sequence = 0
            }));

            Guid user2Id = Guid.NewGuid();
            var @event2 = new UserCreated(user2Id, "User2", null, "password");

            _repository.OnNext(new EventStoreChanged(new StoredEvent()
            {
                StoredEventId = Guid.NewGuid(),
                Aggregate = type.Name,
                AggregateDotNetType = type.AssemblyQualifiedName,
                Data = JsonConvert.SerializeObject(@event2),
                StreamId = user2Id,
                DotNetType = @event2.GetType().AssemblyQualifiedName,
                Type = @event2.GetType().Name,
                CreatedOn = DateTime.UtcNow,
                Sequence = 1
            }));

            var aggregates = _repository.Query<User>();

            var a = _repository.Query<User>(user1Id);
        }


        [Fact]
        public void CanQueryAllAggregates()
        {

            _repository = new Repository(new MockEventStore(new Dictionary<string, IEnumerable<object>>()));
        }
    }
}
