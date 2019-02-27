using CodeWithQB.Core.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Videos
{
    public class RemoveVideoCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.VideoId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid VideoId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var video = await _context.Videos.FindAsync(request.VideoId);

                _context.Videos.Remove(video);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
