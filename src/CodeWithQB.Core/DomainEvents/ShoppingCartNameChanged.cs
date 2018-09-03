using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ShoppingCartNameChanged: DomainEvent
    {
        public ShoppingCartNameChanged(string name)
        {

            Name = name;
        }

        public string Name { get; set; }
    }
}
