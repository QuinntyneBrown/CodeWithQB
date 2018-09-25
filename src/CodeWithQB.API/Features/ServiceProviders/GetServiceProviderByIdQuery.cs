using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.ServiceProviders
{
    public class GetServiceProviderByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ServiceProviderId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid ServiceProviderId { get; set; }
        }

        public class Response
        {
            public ServiceProviderDto ServiceProvider { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepository _repository;
            
			public Handler(IRepository repository) => _repository = repository;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
			     => Task.FromResult(new Response()
                {
                    ServiceProvider = ServiceProviderDto.FromServiceProvider(_repository.Query<ServiceProvider>().Single(x => x.ServiceProviderId == request.ServiceProviderId))
                });
        }
    }
}
