using CodeWithQB.Core.Common;
using CodeWithQB.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeWithQB.Core.Interfaces
{
    public interface IEventStore 
    {
        void Save(Entity aggregateRoot);

        TAggregateRoot Load<TAggregateRoot>(Guid id)
            where TAggregateRoot : Entity;

        void Subscribe(Action<EventStoreChanged> onNext);

        Task<IEnumerable<StoredEvent>> GetEvents();
    }
}
