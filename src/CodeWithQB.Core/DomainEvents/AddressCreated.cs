using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class AddressCreated: DomainEvent
    {
        public AddressCreated(Guid addressId, string addressLine1)
        {
            AddressId = addressId;
            AddressLine1 = addressLine1;
        }

        public Guid AddressId { get; set; }
        public string AddressLine1 { get; set; }
    }
}
