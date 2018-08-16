using System;

namespace CodeWithQB.Core.DomainEvents
{
    public class DashboardCardCreated: DomainEvent
    {
        public DashboardCardCreated(Guid dashboardCardId, Guid dashboardId, Guid cardId) {
            DashboardId = dashboardId;
            CardId = cardId;
            DashboardCardId = dashboardCardId;
        }
        public Guid DashboardId { get; set; }
        public Guid DashboardCardId { get; set; }
        public Guid CardId { get; set; }
    }
}
