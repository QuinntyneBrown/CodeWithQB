using CodeWithQB.Core.Common;
using System;

namespace CodeWithQB.Core.Interfaces
{
    public interface IRepository
    {
        TAggregateRoot[] Query<TAggregateRoot>() where TAggregateRoot : AggregateRoot;

        TAggregateRoot Query<TAggregateRoot>(Guid id) where TAggregateRoot : AggregateRoot;
    }
}
