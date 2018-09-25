using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class Attendee: Entity
    {
        public Attendee(string firstName)
            => Apply(new AttendeeCreated(AttendeeId,firstName));

        public Guid AttendeeId { get; set; } = Guid.NewGuid();    
        public Guid EventId { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AttendeeStatus Status { get; set; }
        public int Version { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case AttendeeCreated created:
                    
                    AttendeeId = created.AttendeeId;
                    break;
                    
                case AttendeeRemoved removed:
                    Status = AttendeeStatus.InActive;
                    Version++;
                    break;
            }
        }

        public void Remove()
            => Apply(new AttendeeRemoved());
    }

    public enum AttendeeStatus
    {
        Active,
        InActive
    }
}
