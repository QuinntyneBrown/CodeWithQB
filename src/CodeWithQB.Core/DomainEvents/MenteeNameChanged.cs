using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class MenteeNameChanged: DomainEvent
    {
        public MenteeNameChanged(string firstName)
        {
             FirstName = firstName;
        }

        public string FirstName { get; set; }
    }
}
