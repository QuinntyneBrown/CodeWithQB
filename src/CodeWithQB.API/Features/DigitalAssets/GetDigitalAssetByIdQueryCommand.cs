using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CodeWithQB.Core.Interfaces;
using System;

namespace CodeWithQB.Api.Features.DigitalAssets
{
    public class GetDigitalAssetByIdQuery
    {
        public class Request : IRequest<Response> {
            public Guid DigitalAssetId { get; set; }
        }

        public class Response
        {
            public DigitalAssetDto DigitalAsset { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    DigitalAsset = (await _context.DigitalAssets.FindAsync(request.DigitalAssetId)).ToDto()
                };
        }
    }
}
