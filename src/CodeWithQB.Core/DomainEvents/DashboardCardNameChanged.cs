namespace CodeWithQB.Core.DomainEvents
{
    public class DashboardCardNameChanged: DomainEvent
    {
        public DashboardCardNameChanged(string name) => Name = name;
        public string Name { get; set; }
    }
}
