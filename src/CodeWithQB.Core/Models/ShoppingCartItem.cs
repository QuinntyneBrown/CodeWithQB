using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class ShoppingCartItem: AggregateRoot
    {
        public ShoppingCartItem(string name)
            => Apply(new ShoppingCartItemCreated(ShoppingCartItemId, name));

        public Guid ShoppingCartItemId { get; set; } = Guid.NewGuid();          
		public string Name { get; set; }        
		public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case ShoppingCartItemCreated shoppingCartItemCreated:
                    Name = shoppingCartItemCreated.Name;
					ShoppingCartItemId = shoppingCartItemCreated.ShoppingCartItemId;
                    break;

                case ShoppingCartItemNameChanged shoppingCartItemNameChanged:
                    Name = shoppingCartItemNameChanged.Name;
                    break;

                case ShoppingCartItemRemoved shoppingCartItemRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new ShoppingCartItemNameChanged(name));

        public void Remove()
            => Apply(new ShoppingCartItemRemoved());
    }
}
