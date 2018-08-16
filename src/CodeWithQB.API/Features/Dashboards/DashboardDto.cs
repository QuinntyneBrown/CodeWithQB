using CodeWithQB.API.Features.DashboardCards;
using CodeWithQB.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWithQB.API.Features.Dashboards
{
    public class DashboardDto
    {        
        public Guid DashboardId { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public ICollection<DashboardCardDto> DashboardCards { get; set; }
        
        public static DashboardDto FromDashboard(
            Dashboard dashboard,
            ICollection<DashboardCardDto> dashboardCards = default(ICollection<DashboardCardDto>))
            => new DashboardDto
            {
                DashboardId = dashboard.DashboardId,
                Name = dashboard.Name,
                UserId = dashboard.UserId,
                DashboardCards = dashboardCards != default(ICollection<DashboardCardDto>) 
                ? dashboardCards
                : new List<DashboardCardDto>()
            };
    }
}
