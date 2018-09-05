using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using CodeWithQB.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CodeWithQB.Core.Common;
using CodeWithQB.Core.Models;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    public class GetCurrentShoppingCartCommand
    {
        public class Request : AuthenticatedRequest<Response> { }

        public class Response
        {
            public ShoppingCartDto ShoppingCart { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IEventStore _eventStore { get; set; }
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var shoppingCart = _eventStore.Query<ShoppingCart>()
                    .SingleOrDefault(x => x.UserId == request.CurrentUserId && x.Status == ShoppingCartStatus.Shopping);

                if (shoppingCart != null) return new Response() { ShoppingCart = ShoppingCartDto.FromShoppingCart(shoppingCart) };

                return new Response() { };
            }
        }
    }
}
