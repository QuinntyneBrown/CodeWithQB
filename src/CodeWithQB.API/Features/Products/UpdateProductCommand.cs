using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace CodeWithQB.API.Features.Products
{
    public class UpdateProductCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Product.ProductId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ProductDto Product { get; set; }
        }

        public class Response
        {            
            public Guid ProductId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var product = _eventStore.Query<Product>().Single(x => x.ProductId == request.Product.ProductId);

                product.ChangeName(request.Product.Name);

                _eventStore.Save(product);

                return Task.FromResult(new Response() { ProductId = request.Product.ProductId }); 
            }
        }
    }
}
