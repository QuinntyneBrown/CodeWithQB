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
    public class GetShoppingCartItemByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ShoppingCartItemId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid ShoppingCartItemId { get; set; }
        }

        public class Response
        {
            public ShoppingCartItemDto ShoppingCartItem { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
			     => Task.FromResult(new Response()
                {
                    ShoppingCartItem = ShoppingCartItemDto.FromShoppingCartItem(_eventStore.Query<ShoppingCartItem>().Single(x => x.ShoppingCartItemId == request.ShoppingCartItemId))
                });
        }
    }
}
