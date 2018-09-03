using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ShoppingCartCreated: DomainEvent
    {
        public ShoppingCartCreated(Guid shoppingCartId, string name)
        {
            ShoppingCartId = shoppingCartId;
            Name = name;
        }

		public Guid ShoppingCartId { get; set; }
        public string Name { get; set; }
    }
}
