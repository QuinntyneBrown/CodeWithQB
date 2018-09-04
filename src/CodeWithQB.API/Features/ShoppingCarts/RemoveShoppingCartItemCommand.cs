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
    public class RemoveShoppingCartItemCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ShoppingCartItemId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Guid ShoppingCartItemId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
				var shoppingCartItem = _eventStore.Query<ShoppingCartItem>().Single(x => x.ShoppingCartItemId == request.ShoppingCartItemId);

                shoppingCartItem.Remove();
                
                _eventStore.Save(shoppingCartItem);

                return Task.CompletedTask;
            }
        }
    }
}
