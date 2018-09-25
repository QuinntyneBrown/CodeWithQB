using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class BookingCreated: DomainEvent
    {
        public BookingCreated(Guid bookingId, string name)
        {
            BookingId = bookingId;
            Name = name;
        }

		public Guid BookingId { get; set; }
        public string Name { get; set; }
    }
}
