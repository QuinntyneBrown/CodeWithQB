using CodeWithQB.Core.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Locations
{
    public class RemoveLocationCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.LocationId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid LocationId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var location = await _context.Locations.FindAsync(request.LocationId);

                _context.Locations.Remove(location);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
