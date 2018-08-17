using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ProductNameChanged: DomainEvent
    {
        public ProductNameChanged(string name)
        {
             Name = name;
        }

        public string Name { get; set; }
    }
}
