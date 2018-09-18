using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using System;
using System.Collections.Generic;

namespace CodeWithQB.Core.Models
{
    public class Dashboard: Entity
    {
        public Dashboard(string name, Guid userId)
            => Apply(new DashboardCreated(name,DashboardId,userId));

        public Guid DashboardId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public ICollection<Guid> DashboardCardIds { get; set; }
        public string Name { get; set; }
        public DashboardStatus Status { get; set; }
        public int Version { get; set; }
        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case DashboardCreated dashboardCreated:
                    Name = dashboardCreated.Name;
                    UserId = dashboardCreated.UserId;
                    DashboardId = dashboardCreated.DashboardId;
                    DashboardCardIds = new HashSet<Guid>();
                    break;
                    
                case DashboardNameChanged dashboardNameChanged:
                    Name = dashboardNameChanged.Name;
                    Version++;
                    break;

                case DashboardRemoved dashboardRemoved:
                    Status = DashboardStatus.InActive;
                    Version++;
                    break;

                case DashboardCardAddedToDashboard dashboardCardAddedToDashboard:
                    DashboardCardIds.Add(dashboardCardAddedToDashboard.DashboardCardId);
                    Version++;
                    break;

                case DashboardCardRemovedFromDashboard dashboardCardRemovedFromDashboard:
                    DashboardCardIds.Remove(dashboardCardRemovedFromDashboard.DashboardCardId);
                    Version++;
                    break;
            }
        }

        public void ChangeName(string name)
            => Apply(new DashboardNameChanged(name));

        public void Remove()
            => Apply(new DashboardRemoved());

        public void AddDashboardCard(Guid dashboardCardId) {            
            Apply(new DashboardCardAddedToDashboard(dashboardCardId));
        }

        public void RemoveDashboardCard(Guid dashboardCardId)
        {
            if (!DashboardCardIds.Contains(dashboardCardId)) throw new Exception();

            Apply(new DashboardCardRemovedFromDashboard(dashboardCardId));
        }
    }

    public enum DashboardStatus
    {
        Active,
        InActive
    }
}
