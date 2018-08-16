using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class MenteeCreated: DomainEvent
    {
        public MenteeCreated(string name, Guid menteeId)
        {
            MenteeId = menteeId;
            FirstName = name;
        }

        public string FirstName { get; set; }
        public Guid MenteeId { get; set; }
    }
}
