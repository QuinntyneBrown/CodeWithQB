using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ShoppingCartItemAdded: DomainEvent
    {
        public ShoppingCartItemAdded(Guid productId)
        {
            ProductId = productId;
        }

        public Guid ProductId { get; set; }
    }
}
