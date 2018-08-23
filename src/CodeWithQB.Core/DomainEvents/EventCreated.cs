using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class EventCreated: DomainEvent
    {
        public EventCreated(Guid eventId, string name)
        {
            EventId = eventId;
            Name = name;
        }

        public Guid EventId { get; set; }
        public string Name { get; set; }
    }
}
