using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class GuestNameChanged: DomainEvent
    {
        public GuestNameChanged(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
