using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace CodeWithQB.API.Features.Attendees
{
    public class RemoveAttendeeCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.AttendeeId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest
        {
            public Guid AttendeeId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task Handle(Request request, CancellationToken cancellationToken)
            {
				var attendee = _eventStore.Load<Attendee>(request.AttendeeId);

                attendee.Remove();
                
                _eventStore.Save(attendee);

                return Task.CompletedTask;
            }
        }
    }
}
