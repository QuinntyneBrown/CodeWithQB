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
            private readonly IDateTime _dateTime;

            public Handler(IDateTime dateTime, IEventStore eventStore)
            {

                _eventStore = eventStore;
                _dateTime = dateTime;
            }

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var shoppingCart = _eventStore.Load<ShoppingCart>(request.ShoppingCartId);

                if (shoppingCart == null)
                    shoppingCart = new ShoppingCart(request.CurrentUserId, _dateTime.UtcNow);

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
