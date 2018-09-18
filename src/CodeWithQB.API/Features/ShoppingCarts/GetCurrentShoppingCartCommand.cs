using CodeWithQB.Core.Common;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            private readonly IRepository _repository;
            public Handler(IRepository repository) => _repository = repository;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var shoppingCart = _repository.Query<ShoppingCart>()
                    .SingleOrDefault(x => x.UserId == request.CurrentUserId && x.Status == ShoppingCartStatus.Shopping);

                if (shoppingCart != null) return new Response() { ShoppingCart = ShoppingCartDto.FromShoppingCart(shoppingCart) };

                return new Response() { };
            }
        }
    }
}
