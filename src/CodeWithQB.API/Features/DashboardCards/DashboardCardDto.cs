using CodeWithQB.API.Features.Cards;
using CodeWithQB.Core.Models;
using Newtonsoft.Json;
using System;

namespace CodeWithQB.API.Features.DashboardCards
{
    public class DashboardCardDto
    {        
        public Guid DashboardCardId { get; set; }
        public Guid DashboardId { get; set; }
        public Guid CardId { get; set; }
        public CardDto Card { get; set; }
        public OptionsDto Options { get; set; }
        public static DashboardCardDto FromDashboardCard(DashboardCard dashboardCard)
            => new DashboardCardDto
            {
                DashboardCardId = dashboardCard.DashboardCardId,
                DashboardId = dashboardCard.DashboardId,
                CardId = dashboardCard.CardId,
                Options = JsonConvert.DeserializeObject<OptionsDto>(dashboardCard.Options)
            };

        public class OptionsDto
        {
            public int Top { get; set; } = 1;
            public int Left { get; set; } = 1;
            public int Height { get; set; } = 1;
            public int Width { get; set; } = 1;
        }
    }
}
