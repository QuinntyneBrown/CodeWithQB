using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;

namespace CodeWithQB.Core.Models
{
    public class Card: AggregateRoot
    {
        public Card(string name)
            => Apply(new CardCreated(name,CardId));

        public Guid CardId { get; set; } = Guid.NewGuid();          
        public string Name { get; set; }        
        public bool IsDeleted { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case CardCreated cardCreated:
                    Name = cardCreated.Name;
                    CardId = cardCreated.CardId;
                    break;

                case CardNameChanged cardNameChanged:
                    Name = cardNameChanged.Name;
                    break;

                case CardRemoved cardRemoved:
                    IsDeleted = true;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new CardNameChanged(name));

        public void Remove()
            => Apply(new CardRemoved());
    }
}
