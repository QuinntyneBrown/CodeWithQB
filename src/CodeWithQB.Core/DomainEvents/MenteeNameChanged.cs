using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class MenteeNameChanged: DomainEvent
    {
        public MenteeNameChanged(string name)
        {
             Name = name;
        }

        public string Name { get; set; }
    }
}
