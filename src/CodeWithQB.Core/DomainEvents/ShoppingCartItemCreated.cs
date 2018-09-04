using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ShoppingCartItemCreated: DomainEvent
    {
        public ShoppingCartItemCreated(Guid shoppingCartItemId)
        {
            ShoppingCartItemId = shoppingCartItemId;
        }

		public Guid ShoppingCartItemId { get; set; }        
    }
}
