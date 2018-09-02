using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class Event: AggregateRoot
    {
        public Event(string name)
            => Apply(new EventCreated(EventId,name));

        public Guid EventId { get; set; } = Guid.NewGuid();          
        public string Name { get; set; }
        public Guid AddressId { get; set; }
        public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case EventCreated eventCreated:
                    Name = eventCreated.Name;
                    EventId = eventCreated.EventId;
                    break;

                case EventNameChanged eventNameChanged:
                    Name = eventNameChanged.Name;
                    break;

                case EventRemoved eventRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new EventNameChanged(name));

        public void Remove()
            => Apply(new EventRemoved());
    }
}
