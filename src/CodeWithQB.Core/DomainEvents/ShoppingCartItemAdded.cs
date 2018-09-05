using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ShoppingCartItemAdded: DomainEvent
    {
        public ShoppingCartItemAdded(Guid shoppingCartItemId)
        {
            ShoppingCartItemId = shoppingCartItemId;
        }

        public Guid ShoppingCartItemId { get; set; }
    }
}
