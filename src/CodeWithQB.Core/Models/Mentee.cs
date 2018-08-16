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
		public string Name { get; set; }        
		public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case MenteeCreated menteeCreated:
                    Name = menteeCreated.Name;
					MenteeId = menteeCreated.MenteeId;
                    break;

                case MenteeNameChanged menteeNameChanged:
                    Name = menteeNameChanged.Name;
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
