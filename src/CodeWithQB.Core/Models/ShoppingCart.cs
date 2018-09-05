using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWithQB.Core.Models
{
    public class ShoppingCart: AggregateRoot
    {
        public ShoppingCart(Guid userId)
            => Apply(new ShoppingCartCreated(ShoppingCartId, userId));

        public Guid ShoppingCartId { get; set; } = Guid.NewGuid(); 
        public ICollection<Guid> ShoppingCartItemIds { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public Guid UserId { get; set; }
        public ShoppingCartStatus Status { get; set; }
        public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {

        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case ShoppingCartCreated shoppingCartCreated:                    
					ShoppingCartId = shoppingCartCreated.ShoppingCartId;
                    UserId = shoppingCartCreated.UserId;
                    Status = ShoppingCartStatus.Shopping;
                    ShoppingCartItems = new List<ShoppingCartItem>();
                    break;

                case ShoppingCartItemAdded shoppingCartItemAdded:
                    
                    var item = new ShoppingCartItem(ShoppingCartId, shoppingCartItemAdded.ProductId, 1);

                    if (ShoppingCartItems.Contains(item))
                        throw new Exception();

                    ShoppingCartItems.Add(item);                    
                    break;

                case ShoppingCartCheckedOut shoppingCartCheckedOut:
                    Status = ShoppingCartStatus.CheckedOut;
                    break;

                case ShoppingCartRemoved shoppingCartRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void AddShoppingCartItem(Guid productId)
            => Apply(new ShoppingCartItemAdded(productId));

        public void Checkout()
            => Apply(new ShoppingCartCheckedOut());

        public void Remove()
            => Apply(new ShoppingCartRemoved());
    }

    public enum ShoppingCartStatus
    {
        Shopping = 0,
        CheckedOut = 1
    }
}
