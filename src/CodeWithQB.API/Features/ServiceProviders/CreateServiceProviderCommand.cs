using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace CodeWithQB.API.Features.ServiceProviders
{
    public class CreateServiceProviderCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ServiceProvider.ServiceProviderId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ServiceProviderDto ServiceProvider { get; set; }
        }

        public class Response
        {			
            public Guid ServiceProviderId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var serviceProvider = new ServiceProvider(request.ServiceProvider.FirstName);

                _eventStore.Save(serviceProvider);
                
                return Task.FromResult(new Response() { ServiceProviderId = serviceProvider.ServiceProviderId });
            }
        }
    }
}
