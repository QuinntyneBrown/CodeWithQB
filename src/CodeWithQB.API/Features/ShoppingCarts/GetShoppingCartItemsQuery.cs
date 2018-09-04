using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    public class GetShoppingCartItemsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ShoppingCartItemDto> ShoppingCartItems { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;

            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => Task.FromResult(new Response()
                {
                    ShoppingCartItems = _eventStore.Query<ShoppingCartItem>().Select(x => ShoppingCartItemDto.FromShoppingCartItem(x)).ToList()
                });
        }
    }
}
