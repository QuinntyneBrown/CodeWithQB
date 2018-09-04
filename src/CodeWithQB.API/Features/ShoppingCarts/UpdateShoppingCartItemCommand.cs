using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    public class UpdateShoppingCartItemCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ShoppingCartItem.ShoppingCartItemId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ShoppingCartItemDto ShoppingCartItem { get; set; }
        }

        public class Response
        {			
            public Guid ShoppingCartItemId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {                
				var shoppingCartItem = _eventStore.Query<ShoppingCartItem>().Single(x => x.ShoppingCartItemId == request.ShoppingCartItem.ShoppingCartItemId);
                
                _eventStore.Save(shoppingCartItem);

                return Task.FromResult(new Response() { ShoppingCartItemId = request.ShoppingCartItem.ShoppingCartItemId }); 
            }
        }
    }
}
