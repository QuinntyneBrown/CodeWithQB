using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class Mentee: AggregateRoot
    {
        public Mentee(string name)
            => Apply(new MenteeCreated(name,MenteeId));

        public Guid MenteeId { get; set; } = Guid.NewGuid();          
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsDeleted { get; set; }

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

        public void ChangeName(string name)
            => Apply(new MenteeNameChanged(name));

        public void Remove()
            => Apply(new MenteeRemoved());
    }
}
