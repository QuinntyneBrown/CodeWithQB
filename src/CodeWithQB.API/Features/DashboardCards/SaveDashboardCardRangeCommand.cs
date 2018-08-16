using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.DashboardCards
{
    public class SaveDashboardCardRangeCommand
    {
        public class Request : IRequest<Response> {
            public IEnumerable<DashboardCardDto> DashboardCards { get; set; }
        }

        public class Response
        {
            public IEnumerable<Guid> DashboardCardIds { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IMediator _mediator;

            public Handler(IMediator mediator) => _mediator = mediator;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboardCardIds = new List<Guid>();

                foreach (var dashboardCard in request.DashboardCards)
                {
                    var response = await _mediator.Send(new CreateDashboardCardCommand.Request() { DashboardCard = dashboardCard });
                    dashboardCardIds.Add(response.DashboardCardId);
                }

                return new Response()
                {
                    DashboardCardIds = dashboardCardIds
                };
            }
        }
    }
}
