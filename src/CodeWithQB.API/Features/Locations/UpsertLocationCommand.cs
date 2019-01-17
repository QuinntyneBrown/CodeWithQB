using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Locations
{
    public class UpsertLocationCommand
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Location.LocationId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public LocationDto Location { get; set; }
        }

        public class Response
        {
            public Guid LocationId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var location = await _context.Locations.FindAsync(request.Location.LocationId);

                if (location == null) {
                    location = new Location();
                    _context.Locations.Add(location);
                }

                location.Name = request.Location.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { LocationId = location.LocationId };
            }
        }
    }
}
