using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class Product: AggregateRoot
    {
        public Product(string name)
            => Apply(new ProductCreated(ProductId,name));

        public Guid ProductId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }
        public string Decription { get; set; }
        public float Price { get; set; }
        public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case ProductCreated productCreated:
                    Name = productCreated.Name;
					ProductId = productCreated.ProductId;
                    break;

                case ProductNameChanged productNameChanged:
                    Name = productNameChanged.Name;
                    break;

                case ProductRemoved productRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new ProductNameChanged(name));

        public void Remove()
            => Apply(new ProductRemoved());
    }
}
