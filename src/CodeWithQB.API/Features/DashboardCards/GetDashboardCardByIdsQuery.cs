using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using CodeWithQB.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CodeWithQB.Core.Models;
using System;
using CodeWithQB.API.Features.Cards;

namespace CodeWithQB.API.Features.DashboardCards
{
    public class GetDashboardCardByIdsQuery
    {
        public class Request : IRequest<Response> {
            public Guid[] DashboardCardIds { get; set; }
        }

        public class Response
        {
            public IEnumerable<DashboardCardDto> DashboardCards { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;

            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboardCards = new List<DashboardCardDto>();

                foreach(var id in request.DashboardCardIds)
                {
                    var dashboardCard = DashboardCardDto.FromDashboardCard(_eventStore.Query<DashboardCard>(id));
                    dashboardCard.Card = CardDto.FromCard(_eventStore.Query<Card>(dashboardCard.CardId));
                    dashboardCards.Add(dashboardCard);
                }

                return Task.FromResult(new Response()
                {
                    DashboardCards = dashboardCards.ToArray()
                });
            }
        }
    }
}
