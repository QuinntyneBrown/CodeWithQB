using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace CodeWithQB.API.Features.DashboardCards
{
    public class CreateDashboardCardCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.DashboardCard.DashboardCardId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public DashboardCardDto DashboardCard { get; set; }
        }

        public class Response
        {			
            public Guid DashboardCardId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboardCard = new DashboardCard(request.DashboardCard.DashboardId,request.DashboardCard.CardId);

                _eventStore.Save(dashboardCard);

                var dashboard = _eventStore.Query<Dashboard>().Single(x => x.DashboardId == request.DashboardCard.DashboardId);

                dashboard.AddDashboardCard(dashboardCard.DashboardCardId);

                _eventStore.Save(dashboard);

                return Task.FromResult(new Response() { DashboardCardId = dashboardCard.DashboardCardId });
            }
        }
    }
}
