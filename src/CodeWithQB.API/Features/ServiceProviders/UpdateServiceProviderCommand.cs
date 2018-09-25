using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace CodeWithQB.API.Features.ServiceProviders
{
    public class UpdateServiceProviderCommand
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
				var serviceProvider = _eventStore.Load<ServiceProvider>(request.ServiceProvider.ServiceProviderId);
                
                _eventStore.Save(serviceProvider);

                return Task.FromResult(new Response() { ServiceProviderId = request.ServiceProvider.ServiceProviderId }); 
            }
        }
    }
}
