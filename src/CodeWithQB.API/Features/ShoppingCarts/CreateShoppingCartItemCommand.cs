using CodeWithQB.Core.Common;
using CodeWithQB.Core.Exceptions;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            public ShoppingCartDto ShoppingCart { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var shoppingCart = _eventStore.Load<ShoppingCart>(request.ShoppingCartId);

                if (shoppingCart == null)
                    shoppingCart = new ShoppingCart(request.CurrentUserId);

                if (shoppingCart.Status != ShoppingCartStatus.Shopping)
                    throw new Exception();

                if (shoppingCart != null && shoppingCart.Version != request.Version)
                    throw new ConcurrencyException();
              
                shoppingCart.AddShoppingCartItem(request.ProductId);
                
                _eventStore.Save(shoppingCart);
                
                return Task.FromResult(new Response() {
                    ShoppingCart = ShoppingCartDto.FromShoppingCart(shoppingCart)
                });
            }
        }
    }
}
