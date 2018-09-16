using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class Product: AggregateRoot
    {
        public Product(string name, float price, string description)
            => Apply(new ProductCreated(ProductId,name, price, description));

        public Guid ProductId { get; set; } = Guid.NewGuid();          
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Version { get; set; }

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
                    Price = productCreated.Price;
                    Description = productCreated.Description;
                    ProductId = productCreated.ProductId;
                    break;

                case ProductUpdated productUpdated:
                    Name = productUpdated.Name;
                    Description = productUpdated.Description;
                    break;

                case ProductRemoved productRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void Update(string name, string description)
            => Apply(new ProductUpdated(name, description));


        public void Remove()
            => Apply(new ProductRemoved());
    }
}
