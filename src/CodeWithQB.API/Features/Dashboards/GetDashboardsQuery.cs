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
            private readonly IRepository _repository;

            public Handler(IRepository repository) => _repository = repository;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => Task.FromResult(new Response()
                {
                    Dashboards = _repository.Query<Dashboard>().Select(x => DashboardDto.FromDashboard(x)).ToList()
                });
        }
    }
}
