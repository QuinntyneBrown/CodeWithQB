using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace CodeWithQB.API.Features.Events
{
    public class RemoveEventCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.EventId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Guid EventId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
                var @event = _eventStore.Load<Event>(request.EventId);

                @event.Remove();
                
                _eventStore.Save(@event);

                return Task.CompletedTask;
            }
        }
    }
}
