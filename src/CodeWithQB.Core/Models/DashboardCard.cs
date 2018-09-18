using CodeWithQB.Core.Common;
using CodeWithQB.Core.DomainEvents;
using Newtonsoft.Json;
using System;

namespace CodeWithQB.Core.Models
{
    public class DashboardCard: Entity
    {
        public DashboardCard(Guid dashoardId, Guid cardId)
            => Apply(new DashboardCardCreated(DashboardCardId,dashoardId,cardId));

        public Guid DashboardCardId { get; set; } = Guid.NewGuid();
        public Guid DashboardId { get; set; }
        public Guid CardId { get; set; }
        public string Options { get; set; }
        public int Version { get; set; }
        public DashboardCardStatus Status { get; set; }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case DashboardCardCreated dashboardCardCreated:
                    DashboardId = dashboardCardCreated.DashboardId;
                    CardId = dashboardCardCreated.CardId;
                    DashboardCardId = dashboardCardCreated.DashboardCardId;
                    Options = "{}";                    
                    break;

                case DashboardCardRemoved dashboardCardRemoved:
                    Status = DashboardCardStatus.InActive;
                    Version++;
                    break;

                case DashboardCardOptionsUpdated dashboardCardOptionsUpdated:
                    Options = JsonConvert.SerializeObject(new {
                        dashboardCardOptionsUpdated.Top,
                        dashboardCardOptionsUpdated.Height,
                        dashboardCardOptionsUpdated.Width,
                        dashboardCardOptionsUpdated.Left
                    });
                    Version++;
                    break;
            }
        }
        
        public void Remove()
            => Apply(new DashboardCardRemoved());

        public void UpdateOptions(int top, int width, int left, int height)
            => Apply(new DashboardCardOptionsUpdated(top, left, height, width));
    }

    public enum DashboardCardStatus
    {
        Active,
        InActive
    }
}
