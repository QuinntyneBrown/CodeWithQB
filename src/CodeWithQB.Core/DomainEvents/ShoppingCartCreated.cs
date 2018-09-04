using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ShoppingCartCreated: DomainEvent
    {
        public ShoppingCartCreated(Guid shoppingCartId, Guid userId)
        {
            ShoppingCartId = shoppingCartId;
            UserId = userId;
        }

		public Guid ShoppingCartId { get; set; }
        public Guid UserId { get; set; }
    }
}
