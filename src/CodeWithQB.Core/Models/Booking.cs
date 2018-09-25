using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class Booking: Entity
    {
        public Booking(string name)
            => Apply(new BookingCreated(BookingId, name));

        public Guid BookingId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }        
		public BookingStatus Status { get; set; }
		public int Version { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case BookingCreated bookingCreated:
                    Name = bookingCreated.Name;
					BookingId = bookingCreated.BookingId;
					Status = BookingStatus.Active;
                    break;

                case BookingNameChanged bookingNameChanged:
                    Name = bookingNameChanged.Name;
					Version++;
                    break;

                case BookingRemoved bookingRemoved:
                    Status = BookingStatus.InActive;
					Version++;
                    break;
            }
        }
        
        public void Remove()
            => Apply(new BookingRemoved());
    }

    public enum BookingStatus
    {
        Active,
        InActive
    }
}
