using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class CustomerCreated: DomainEvent
    {
        public CustomerCreated(Guid customerId, string name)
        {
            CustomerId = customerId;
            Name = name;
        }

		public Guid CustomerId { get; set; }
        public string Name { get; set; }
    }
}
