using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ProductCreated: DomainEvent
    {
        public ProductCreated(Guid productId, string name)
        {
            ProductId = productId;
            Name = name;
        }

        public Guid ProductId { get; set; }
        public string Name { get; set; }
    }
}
