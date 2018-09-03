using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ShoppingCartItemNameChanged: DomainEvent
    {
        public ShoppingCartItemNameChanged(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
