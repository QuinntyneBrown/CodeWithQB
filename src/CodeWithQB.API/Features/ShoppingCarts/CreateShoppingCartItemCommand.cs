using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using CodeWithQB.Core.Common;
using System.Linq;
using System.Collections.Generic;
using CodeWithQB.Core.Exceptions;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    public class CreateShoppingCartItemCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ProductId).NotNull();
            }
        }

        public class Request : AuthenticatedCommand<Response> {
            public Guid ShoppingCartId { get; set; }
            public Guid ProductId { get; set; }
            public int Version { get; set; }
        }

        public class Response
        {
            public Guid ShoppingCartId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var shoppingCart = _eventStore.Query<ShoppingCart>()
                    .SingleOrDefault(x => x.ShoppingCartId == request.ShoppingCartId && x.Status == ShoppingCartStatus.Shopping);

                if (shoppingCart != null && shoppingCart.Version != request.Version)
                    throw new ConcurrencyException();

                if (shoppingCart == null) shoppingCart = new ShoppingCart(request.CurrentUserId);
                
                shoppingCart.AddShoppingCartItem(request.ProductId);
                
                _eventStore.Save(shoppingCart);
                
                return Task.FromResult(new Response() {
                    ShoppingCartId = shoppingCart.ShoppingCartId
                });
            }
        }
    }
}
