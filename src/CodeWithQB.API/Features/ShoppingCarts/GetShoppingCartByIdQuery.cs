using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    public class GetShoppingCartByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ShoppingCartId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid ShoppingCartId { get; set; }
        }

        public class Response
        {
            public ShoppingCartDto ShoppingCart { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
			     => Task.FromResult(new Response()
                {
                    ShoppingCart = ShoppingCartDto.FromShoppingCart(_eventStore.Query<ShoppingCart>().Single(x => x.ShoppingCartId == request.ShoppingCartId))
                });
        }
    }
}
