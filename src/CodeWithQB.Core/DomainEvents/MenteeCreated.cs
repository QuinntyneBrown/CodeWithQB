using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class MenteeCreated: DomainEvent
    {
        public MenteeCreated(string name, Guid menteeId)
        {
            MenteeId = menteeId;
            Name = name;
        }

        public string Name { get; set; }
        public Guid MenteeId { get; set; }
    }
}
