using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class DashboardCreated: DomainEvent
    {
        public DashboardCreated(string name, Guid dashboardId, Guid userId)
        {
            Name = name;
            UserId = userId;
            DashboardId = dashboardId;
        }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid DashboardId { get; set; }
    }
}
