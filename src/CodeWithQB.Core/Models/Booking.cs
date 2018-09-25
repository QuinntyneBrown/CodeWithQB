using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace CodeWithQB.Core.Models
{
    public class Booking: Entity
    {
        public Booking(string name)
            => Apply(new BookingCreated(BookingId, name));

        public Guid BookingId { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public Guid ServiceProviderId { get; set; }
        public Guid ParentBookingId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ICollection<Guid> BookingIds { get; set; }
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
					BookingId = bookingCreated.BookingId;
                    BookingIds = new HashSet<Guid>();
					Status = BookingStatus.Active;
                    break;

                case BookingNameChanged bookingNameChanged:
                    
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
