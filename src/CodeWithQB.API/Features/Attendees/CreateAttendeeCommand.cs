using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace CodeWithQB.API.Features.Attendees
{
    public class CreateAttendeeCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Attendee.AttendeeId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public AttendeeDto Attendee { get; set; }
        }

        public class Response
        {			
            public Guid AttendeeId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var attendee = new Attendee(request.Attendee.FirstName);

                _eventStore.Save(attendee);
                
                return Task.FromResult(new Response() { AttendeeId = attendee.AttendeeId });
            }
        }
    }
}
