using CodeWithQB.Core.Common;
using System;

namespace CodeWithQB.Core.Interfaces
{
    public interface IEventStore : IDisposable
    {
        void Save(AggregateRoot aggregateRoot);
        TAggregateRoot Query<TAggregateRoot>(string propertyName, string value)
            where TAggregateRoot : AggregateRoot;
        TAggregateRoot Query<TAggregateRoot>(Guid id)
            where TAggregateRoot : AggregateRoot;
        TAggregateRoot[] Query<TAggregateRoot>()
            where TAggregateRoot : AggregateRoot;
    }
}
