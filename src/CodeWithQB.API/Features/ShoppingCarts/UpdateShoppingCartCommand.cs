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
    public class UpdateShoppingCartCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ShoppingCart.ShoppingCartId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ShoppingCartDto ShoppingCart { get; set; }
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
                var shoppingCart = _eventStore.Load<ShoppingCart>(request.ShoppingCart.ShoppingCartId);
                
                _eventStore.Save(shoppingCart);

                return Task.FromResult(new Response() { ShoppingCartId = request.ShoppingCart.ShoppingCartId }); 
            }
        }
    }
}
