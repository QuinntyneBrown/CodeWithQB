using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace CodeWithQB.API.Features.Events
{
    public class UpdateEventCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Event.EventId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public EventDto Event { get; set; }
        }

        public class Response
        {			
            public Guid EventId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var @event = _eventStore.Query<Event>(request.Event.EventId);

                @event.ChangeName(request.Event.Name);

                _eventStore.Save(@event);

                return Task.FromResult(new Response() { EventId = request.Event.EventId }); 
            }
        }
    }
}
