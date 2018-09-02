using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ProductCreated: DomainEvent
    {
        public ProductCreated(Guid productId, string name, float price, string description)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            Description = description;
        }

        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
    }
}
