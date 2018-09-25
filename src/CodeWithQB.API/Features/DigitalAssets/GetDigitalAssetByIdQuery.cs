using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CodeWithQB.Core.Interfaces;
using FluentValidation;
using System;
using CodeWithQB.Core.Models;

namespace CodeWithQB.API.Features.DigitalAssets
{
    public class GetDigitalAssetByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DigitalAssetId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid DigitalAssetId { get; set; }
        }

        public class Response
        {
            public DigitalAssetDto DigitalAsset { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IRepository _repository { get; set; }
            
            public Handler(IRepository repository) => _repository = repository;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    DigitalAsset = DigitalAssetDto.FromDigitalAsset(_repository.Query<DigitalAsset>(request.DigitalAssetId))
                };
        }
    }
}
