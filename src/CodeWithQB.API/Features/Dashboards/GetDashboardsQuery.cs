using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Dashboards
{
    public class GetDashboardsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<DashboardDto> Dashboards { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;

            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => Task.FromResult(new Response()
                {
                    Dashboards = _eventStore.Query<Dashboard>().Select(x => DashboardDto.FromDashboard(x)).ToList()
                });
        }
    }
}
