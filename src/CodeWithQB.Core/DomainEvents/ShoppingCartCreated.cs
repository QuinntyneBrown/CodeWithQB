using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ShoppingCartCreated: DomainEvent
    {
        public ShoppingCartCreated(Guid shoppingCartId, Guid userId, DateTime createdOn)
        {
            ShoppingCartId = shoppingCartId;
            UserId = userId;
            CreatedOn = createdOn;
        }

		public Guid ShoppingCartId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
