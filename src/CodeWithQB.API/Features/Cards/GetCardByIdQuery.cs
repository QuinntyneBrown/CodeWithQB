using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Cards
{
    public class GetCardByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.CardId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid CardId { get; set; }
        }

        public class Response
        {
            public CardDto Card { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
			     => Task.FromResult(new Response()
                {
                    Card = CardDto.FromCard(_eventStore.Query<Card>(request.CardId))
                });
        }
    }
}
