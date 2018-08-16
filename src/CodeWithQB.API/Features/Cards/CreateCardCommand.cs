using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace CodeWithQB.API.Features.Cards
{
    public class CreateCardCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Card.CardId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public CardDto Card { get; set; }
        }

        public class Response
        {			
            public Guid CardId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var card = new Card(request.Card.Name);

                _eventStore.Save(card);
                
                return Task.FromResult(new Response() { CardId = card.CardId });
            }
        }
    }
}
