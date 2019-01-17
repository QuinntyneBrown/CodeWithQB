using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CodeWithQB.Core.Models;
using CodeWithQB.Core.Interfaces;
using System;

namespace CodeWithQB.API.Features.DigitalAssets
{
    public class SaveDigitalAssetCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.DigitalAsset.DigitalAssetId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public DigitalAssetApiModel DigitalAsset { get; set; }
        }

        public class Response
        {            
            public Guid DigitalAssetId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var digitalAsset = await _context.DigitalAssets.FindAsync(request.DigitalAsset.DigitalAssetId);

                if (digitalAsset == null) _context.DigitalAssets.Add(digitalAsset = new DigitalAsset());

                digitalAsset.Name = request.DigitalAsset.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { DigitalAssetId = digitalAsset.DigitalAssetId };
            }
        }
    }
}
