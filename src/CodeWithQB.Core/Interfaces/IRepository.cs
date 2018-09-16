using CodeWithQB.Core.Common;
using System;
using System.Collections.Generic;

namespace CodeWithQB.Core.Interfaces
{
    public interface IRepository
    {
        TAggregateRoot[] Query<TAggregateRoot>() where TAggregateRoot : AggregateRoot;

        TAggregateRoot Query<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot;

        TAggregateRoot[] Query<TAggregateRoot>(IEnumerable<Guid> ids) where TAggregateRoot : AggregateRoot;

        void OnNext(EventStoreChanged onNext);
    }
}
