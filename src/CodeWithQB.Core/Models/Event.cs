using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class Event: Entity
    {
        public Event(string name)
            => Apply(new EventCreated(EventId,name));

        public Guid EventId { get; set; } = Guid.NewGuid();          
        public string Name { get; set; }
        public Guid AddressId { get; set; }
        public EventStatus Status { get; set; }
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
