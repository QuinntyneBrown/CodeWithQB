using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace CodeWithQB.Core.Models
{
    public class Event: Entity
    {
        public Event(string name)
            => Apply(new EventCreated(EventId,name));

        public Guid? ParentEventId { get; set; }
        public Guid EventId { get; set; } = Guid.NewGuid();          
        public string Name { get; set; }
        public Address Address { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public EventStatus Status { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public int Version { get; set; }
        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case EventCreated eventCreated:
                    Name = eventCreated.Name;
                    EventId = eventCreated.EventId;
                    Events = new HashSet<Event>();
                    break;

                case EventNameChanged eventNameChanged:
                    Name = eventNameChanged.Name;
                    Version++;
                    break;

                case EventRemoved eventRemoved:
                    Status = EventStatus.InActive;
                    Version++;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new EventNameChanged(name));

        public void Remove()
            => Apply(new EventRemoved());
    }

    public enum EventStatus
    {
        Active,
        InActive
    }
}
