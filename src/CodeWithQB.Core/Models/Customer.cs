using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class Customer: Entity
    {
        public Customer(string name)
            => Apply(new CustomerCreated(CustomerId,name));

        public Guid CustomerId { get; set; } = Guid.NewGuid();          
		public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public CustomerStatus Status { get; set; }
		public int Version { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case CustomerCreated customerCreated:
                    
					CustomerId = customerCreated.CustomerId;
					Status = CustomerStatus.Active;
                    break;

                case CustomerRemoved customerRemoved:
                    Status = CustomerStatus.InActive;
					Version++;
                    break;
            }
        }
        
        public void Remove()
            => Apply(new CustomerRemoved());
    }

    public enum CustomerStatus
    {
        Active,
        InActive
    }
}
