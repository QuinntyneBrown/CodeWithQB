namespace CodeWithQB.Core.DomainEvents
{
    public class DashboardNameChanged: DomainEvent
    {
        public DashboardNameChanged(string name) => Name = name;
        public string Name { get; set; }
    }
}
