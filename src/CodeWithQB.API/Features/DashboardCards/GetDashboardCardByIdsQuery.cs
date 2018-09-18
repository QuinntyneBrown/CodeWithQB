using CodeWithQB.API.Features.Cards;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            private readonly IRepository _repository;

            public Handler(IRepository repository) => _repository = repository;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboardCards = new List<DashboardCardDto>();

                foreach(var id in request.DashboardCardIds)
                {
                    var dashboardCard = DashboardCardDto.FromDashboardCard(_repository.Query<DashboardCard>(id));
                    dashboardCard.Card = CardDto.FromCard(_repository.Query<Card>(dashboardCard.CardId));
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
