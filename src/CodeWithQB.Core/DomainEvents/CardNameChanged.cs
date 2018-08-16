namespace CodeWithQB.Core.DomainEvents
{
    public class CardNameChanged: DomainEvent
    {
        public CardNameChanged(string name) => Name = name;
        public string Name { get; set; }
    }
}

