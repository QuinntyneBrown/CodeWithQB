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

        public class Request : AuthenticatedRequest<Response>, ICommandRequest<Response> {
            public Guid ShoppingCartId { get; set; }
            public Guid ProductId { get; set; }
            public string Key { get; set; }
            public string Partition { get; set; }
            public IEnumerable<string> SideEffects { get; set; } = new List<string>();
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
