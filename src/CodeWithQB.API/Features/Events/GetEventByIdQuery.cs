using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Events
{
    public class GetEventByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.EventId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid EventId { get; set; }
        }

        public class Response
        {
            public EventDto Event { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
			     => Task.FromResult(new Response()
                {
                    Event = EventDto.FromEvent(_eventStore.Query<Event>(request.EventId))
                });
        }
    }
}
