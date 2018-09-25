using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class AttendeeCreated: DomainEvent
    {
        public AttendeeCreated(Guid guestId, string name)
        {
            AttendeeId = guestId;
            FirstName = name;
        }

        public Guid AttendeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
