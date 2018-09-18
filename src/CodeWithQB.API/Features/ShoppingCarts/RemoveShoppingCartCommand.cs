using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    public class RemoveShoppingCartCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ShoppingCartId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Guid ShoppingCartId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
                var shoppingCart = _eventStore.Load<ShoppingCart>(request.ShoppingCartId);

                shoppingCart.Remove();
                
                _eventStore.Save(shoppingCart);

                return Task.CompletedTask;
            }
        }
    }
}
