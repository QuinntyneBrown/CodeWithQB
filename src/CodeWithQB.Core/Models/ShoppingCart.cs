using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class ShoppingCart: AggregateRoot
    {
        public ShoppingCart(string name)
            => Apply(new ShoppingCartCreated(ShoppingCartId,name));

        public Guid ShoppingCartId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }
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
                    Name = shoppingCartCreated.Name;
					ShoppingCartId = shoppingCartCreated.ShoppingCartId;
                    break;

                case ShoppingCartNameChanged shoppingCartNameChanged:
                    Name = shoppingCartNameChanged.Name;
                    break;

                case ShoppingCartRemoved shoppingCartRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new ShoppingCartNameChanged(name));

        public void Remove()
            => Apply(new ShoppingCartRemoved());
    }
}
