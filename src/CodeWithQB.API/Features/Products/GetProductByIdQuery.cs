using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Products
{
    public class GetProductByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProductId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid ProductId { get; set; }
        }

        public class Response
        {
            public ProductDto Product { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
			     => Task.FromResult(new Response()
                {
                    Product = ProductDto.FromProduct(_eventStore.Query<Product>(request.ProductId))
                });
        }
    }
}
