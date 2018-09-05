using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ShoppingCartItemCreated: DomainEvent
    {
        public ShoppingCartItemCreated(Guid shoppingCartItemId, Guid productId, int quantity)
        {
            ShoppingCartItemId = shoppingCartItemId;
            ProductId = productId;
            Quantity = quantity;
        }

		public Guid ShoppingCartItemId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
