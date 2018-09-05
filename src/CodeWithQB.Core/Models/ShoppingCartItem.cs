using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class ShoppingCartItem: AggregateRoot
    {
        public ShoppingCartItem(Guid productId, int quantity)
            => Apply(new ShoppingCartItemCreated(ShoppingCartItemId, productId, quantity));

        public Guid ShoppingCartItemId { get; set; } = Guid.NewGuid();
        public Guid ShoppingCardId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case ShoppingCartItemCreated shoppingCartItemCreated:                    
					ShoppingCartItemId = shoppingCartItemCreated.ShoppingCartItemId;
                    ProductId = shoppingCartItemCreated.ProductId;
                    Quantity = shoppingCartItemCreated.Quantity;
                    break;
                    
                case ShoppingCartItemRemoved shoppingCartItemRemoved:
                    IsDeleted = true;
                    break;
            }
        }
        
        public void Remove()
            => Apply(new ShoppingCartItemRemoved());
    }
}
