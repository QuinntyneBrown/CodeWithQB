using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class Mentee: AggregateRoot
    {
        public Mentee(string firstName, string lastName, string emailAddress)
            => Apply(new MenteeCreated(MenteeId, firstName,lastName,emailAddress));

        public Guid MenteeId { get; set; } = Guid.NewGuid();          
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsDeleted { get; set; }
        public int Version { get; set; }
        public MenteeStatus Status { get; set; }        
        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case MenteeCreated menteeCreated:
                    FirstName = menteeCreated.FirstName;
                    MenteeId = menteeCreated.MenteeId;
                    break;

                case MenteeNameChanged menteeNameChanged:
                    FirstName = menteeNameChanged.FirstName;
                    break;

                case MenteeRemoved menteeRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string firstName, string lastName)
            => Apply(new MenteeNameChanged(firstName,lastName));

        public void Remove()
            => Apply(new MenteeRemoved());
    }

    public enum MenteeStatus {

    }
}
