using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ShoppingCartItemCreated: DomainEvent
    {
        public ShoppingCartItemCreated(Guid shoppingCartItemId, string name)
        {
            ShoppingCartItemId = shoppingCartItemId;
            Name = name;
        }

		public Guid ShoppingCartItemId { get; set; }
        public string Name { get; set; }
    }
}
