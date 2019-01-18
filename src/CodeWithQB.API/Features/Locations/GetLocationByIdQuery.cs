using CodeWithQB.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Locations
{
    public class GetLocationByIdQuery
    {
        public class Request : IRequest<Response> {
            public Guid LocationId { get; set; }
        }

        public class Response
        {
            public LocationDto Location { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Location = LocationDto.FromLocation(await _context.Locations.FindAsync(request.LocationId))
                };
        }
    }
}
