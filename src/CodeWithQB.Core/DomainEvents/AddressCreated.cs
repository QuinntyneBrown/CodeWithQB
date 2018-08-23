using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class AddressCreated: DomainEvent
    {
        public AddressCreated(Guid addressId, string name)
        {
            AddressId = addressId;
            Name = name;
        }

        public Guid AddressId { get; set; }
        public string Name { get; set; }
    }
}
