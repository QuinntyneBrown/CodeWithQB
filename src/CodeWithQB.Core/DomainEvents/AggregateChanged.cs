using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class AggregateChanged: DomainEvent
    {
        public Guid AggregateId { get; set; }
    }
}
