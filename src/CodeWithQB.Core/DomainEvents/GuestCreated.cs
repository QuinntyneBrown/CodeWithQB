using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class GuestCreated: DomainEvent
    {
        public GuestCreated(Guid guestId, string name)
        {
            GuestId = guestId;
            Name = name;
        }

        public Guid GuestId { get; set; }
        public string Name { get; set; }
    }
}
