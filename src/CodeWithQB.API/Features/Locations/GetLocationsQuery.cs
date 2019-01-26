using CodeWithQB.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Locations
{
    public class GetLocationsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<LocationDto> Locations { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                =>  new Response()
                {
                    Locations = await _context.Locations.Include(x => x.Address).Select(x => x.ToDto()).ToArrayAsync()
                };
        }
    }
}
