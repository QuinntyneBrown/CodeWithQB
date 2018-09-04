using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class ShoppingCart: AggregateRoot
    {
        public ShoppingCart(Guid userId)
            => Apply(new ShoppingCartCreated(ShoppingCartId, userId));

        public Guid ShoppingCartId { get; set; } = Guid.NewGuid();          		
        public Guid UserId { get; set; }
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
                    break;
                    
                case ShoppingCartRemoved shoppingCartRemoved:
                    IsDeleted = true;
                    break;
            }
        }
        
        public void Remove()
            => Apply(new ShoppingCartRemoved());
    }
}
