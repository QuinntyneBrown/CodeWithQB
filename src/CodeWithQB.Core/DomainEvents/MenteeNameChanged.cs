using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class MenteeNameChanged: DomainEvent
    {
        public MenteeNameChanged(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
