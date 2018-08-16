using CodeWithQB.API.Features.DashboardCards;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Dashboards
{
    public class GetDashboardByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DashboardId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid DashboardId { get; set; }
        }

        public class Response
        {
            public DashboardDto Dashboard { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboard = _eventStore.Query<Dashboard>(request.DashboardId);
                var dashboardCards = new List<DashboardCardDto>();

                foreach (var dashboardCardId in dashboard.DashboardCardIds)
                {
                    dashboardCards.Add(DashboardCardDto.FromDashboardCard(_eventStore.Query<DashboardCard>(dashboardCardId)));
                }

                return Task.FromResult(new Response()
                {
                    Dashboard = DashboardDto.FromDashboard(dashboard, dashboardCards)
                });

            }
        }
    }
}
