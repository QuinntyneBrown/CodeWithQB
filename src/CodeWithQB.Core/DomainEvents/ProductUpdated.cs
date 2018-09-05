using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class ProductUpdated: DomainEvent
    {
        public ProductUpdated(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
