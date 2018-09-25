using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using CodeWithQB.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using CodeWithQB.Core.Models;

namespace CodeWithQB.API.Features.DigitalAssets
{
    public class GetDigitalAssetsByIdsQuery
    {
        public class Request : IRequest<Response> {
            public Guid[] DigitalAssetIds { get; set; }
        }

        public class Response
        {
            public IEnumerable<DigitalAssetDto> DigitalAssets { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IRepository _repository { get; set; }

            public Handler(IRepository repository) => _repository = repository;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    DigitalAssets = _repository.Query<DigitalAsset>()
                    .Where(x => request.DigitalAssetIds.Contains(x.DigitalAssetId))
                    .Select(x => DigitalAssetDto.FromDigitalAsset(x)).ToList()
                };
        }
    }
}
