using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using CodeWithQB.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    public class CheckoutCommand
    {
        public class Request : IRequest<Response> {
            public int ShoppingCartId { get; set; }
        }

        public class Response
        {
            public int ShoppingCartId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
			    return new Response() { };
            }
        }
    }
}
