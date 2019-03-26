using CodeWithQB.Core.Interfaces;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.DigitalAssets
{
    public class RemoveDigitalAssetCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DigitalAssetId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public int DigitalAssetId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                _context.DigitalAssets.Remove(await _context.DigitalAssets.FindAsync(request.DigitalAssetId));
                await _context.SaveChangesAsync(cancellationToken);
                return new Unit();
            }
        }
    }
}
