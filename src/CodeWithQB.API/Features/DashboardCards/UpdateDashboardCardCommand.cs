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
    public class UpdateDashboardCardCommand
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
                var dashboardCard = _eventStore.Load<DashboardCard>(request.DashboardCard.DashboardCardId);

                dashboardCard.UpdateOptions(
                    request.DashboardCard.Options.Top,
                    request.DashboardCard.Options.Width,
                    request.DashboardCard.Options.Left,
                    request.DashboardCard.Options.Height);

                _eventStore.Save(dashboardCard);

                return Task.FromResult(new Response() { DashboardCardId = request.DashboardCard.DashboardCardId }); 
            }
        }
    }
}
