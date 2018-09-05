using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using CodeWithQB.Core.Common;
using System.Linq;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    public class CreateShoppingCartItemCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ShoppingCartItem.ShoppingCartItemId).NotNull();
            }
        }

        public class Request : AuthenticatedRequest<Response> {
            public ShoppingCartItemDto ShoppingCartItem { get; set; }
        }

        public class Response
        {
            public Guid ShoppingCartId { get; set; }
            public Guid ShoppingCartItemId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var shoppingCart = _eventStore.Query<ShoppingCart>()
                    .SingleOrDefault(x => x.ShoppingCartId == request.ShoppingCartItem.ShoppingCartId && x.Status == ShoppingCartStatus.Shopping);

                if (shoppingCart == null) shoppingCart = new ShoppingCart(request.CurrentUserId);
                
                var shoppingCartItem = new ShoppingCartItem(request.ShoppingCartItem.ProductId, 1);
                
                shoppingCart.AddShoppingCartItem(shoppingCartItem.ShoppingCartItemId);

                _eventStore.Save(shoppingCart);

                _eventStore.Save(shoppingCartItem);
              
                return Task.FromResult(new Response() {
                    ShoppingCartId = shoppingCart.ShoppingCartId,
                    ShoppingCartItemId = shoppingCartItem.ShoppingCartItemId
                });
            }
        }
    }
}
