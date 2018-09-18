using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class Guest: Entity
    {
        public Guest(string name)
            => Apply(new GuestCreated(GuestId,name));

        public Guid GuestId { get; set; } = Guid.NewGuid();          
        public string Name { get; set; }        
        public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case GuestCreated guestCreated:
                    Name = guestCreated.Name;
                    GuestId = guestCreated.GuestId;
                    break;

                case GuestNameChanged guestNameChanged:
                    Name = guestNameChanged.Name;
                    break;

                case GuestRemoved guestRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new GuestNameChanged(name));

        public void Remove()
            => Apply(new GuestRemoved());
    }
}
