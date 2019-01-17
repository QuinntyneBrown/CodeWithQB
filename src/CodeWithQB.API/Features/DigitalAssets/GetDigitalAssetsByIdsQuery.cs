using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using CodeWithQB.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodeWithQB.API.Features.DigitalAssets
{
    public class GetDigitalAssetsByIdsQuery
    {
        public class Request : IRequest<Response> {
            public Guid[] DigitalAssetIds { get; set; }
        }

        public class Response
        {
            public IEnumerable<DigitalAssetApiModel> DigitalAssets { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    DigitalAssets = await _context.DigitalAssets
                    .Where(x => request.DigitalAssetIds.Contains(x.DigitalAssetId))
                    .Select(x => DigitalAssetApiModel.FromDigitalAsset(x)).ToListAsync()
                };
        }
    }
}
