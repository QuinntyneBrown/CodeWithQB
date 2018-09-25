using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CodeWithQB.API.Features.ServiceProviders
{
    public class RemoveServiceProviderCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ServiceProviderId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Guid ServiceProviderId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
                var serviceProvider = _eventStore.Load<ServiceProvider>(request.ServiceProviderId);

                serviceProvider.Remove();
                
                _eventStore.Save(serviceProvider);

                return Task.CompletedTask;
            }
        }
    }
}
