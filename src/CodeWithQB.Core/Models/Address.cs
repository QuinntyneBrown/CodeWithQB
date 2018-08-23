using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class Address: AggregateRoot
    {
        public Address(string name)
            => Apply(new AddressCreated(AddressId,name));

        public Guid AddressId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }        
		public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case AddressCreated addressCreated:
                    Name = addressCreated.Name;
					AddressId = addressCreated.AddressId;
                    break;

                case AddressNameChanged addressNameChanged:
                    Name = addressNameChanged.Name;
                    break;

                case AddressRemoved addressRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new AddressNameChanged(name));

        public void Remove()
            => Apply(new AddressRemoved());
    }
}
