using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class MenteeCreated: DomainEvent
    {
        public MenteeCreated(Guid menteeId, string firstName, string lastName, string emailAddress)
        {
            MenteeId = menteeId;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }

        public Guid MenteeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

    }
}
