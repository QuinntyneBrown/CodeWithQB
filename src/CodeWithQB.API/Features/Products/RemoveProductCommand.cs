using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace CodeWithQB.API.Features.Products
{
    public class RemoveProductCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProductId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Guid ProductId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
                var product = _eventStore.Query<Product>(request.ProductId);

                product.Remove();
                
                _eventStore.Save(product);

                return Task.CompletedTask;
            }
        }
    }
}
