using CodeWithQB.Core.Common;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    public class CheckoutCommand
    {
        public class Request : AuthenticatedRequest<Response> {
            public Guid ShoppingCartId { get; set; }
        }

        public class Response
        {
            public Guid ShoppingCartId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var shoppingCart = _eventStore.Load<ShoppingCart>(request.ShoppingCartId);

                if (shoppingCart.Status != ShoppingCartStatus.Shopping)
                    throw new Exception();

                shoppingCart.Checkout();
                
                _eventStore.Save(shoppingCart);

                return await Task.FromResult(new Response() {
                    ShoppingCartId = shoppingCart.ShoppingCartId
                });
            }
        }
    }
}
