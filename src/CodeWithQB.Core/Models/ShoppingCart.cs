using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace CodeWithQB.Core.Models
{
    public class ShoppingCart: Entity
    {
        public ShoppingCart(Guid userId, DateTime createdOn)
            => Apply(new ShoppingCartCreated(ShoppingCartId, userId, createdOn));

        public Guid ShoppingCartId { get; set; } = Guid.NewGuid();         
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public ShoppingCartStatus Status { get; set; }
        public int Version { get; set; }
        
        protected override void EnsureValidState()
        {

        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case ShoppingCartCreated shoppingCartCreated:                    
					ShoppingCartId = shoppingCartCreated.ShoppingCartId;
                    UserId = shoppingCartCreated.UserId;
                    Status = ShoppingCartStatus.Shopping;
                    CreatedOn = shoppingCartCreated.CreatedOn;
                    ShoppingCartItems = new List<ShoppingCartItem>();
                    break;

                case ShoppingCartItemAdded shoppingCartItemAdded:                    
                    var item = new ShoppingCartItem(ShoppingCartId, shoppingCartItemAdded.ProductId, 1);
                    ShoppingCartItems.Add(item);
                    Version++;
                    break;

                case ShoppingCartCheckedOut shoppingCartCheckedOut:
                    Version++;
                    Status = ShoppingCartStatus.CheckedOut;
                    break;

                case ShoppingCartRemoved shoppingCartRemoved:
                    Version++;
                    Status = ShoppingCartStatus.InActive;
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
        CheckedOut = 1,
        InActive = 2
    }
}
