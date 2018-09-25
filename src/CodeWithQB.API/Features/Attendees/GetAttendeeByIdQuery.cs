using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Attendees
{
    public class GetAttendeeByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.AttendeeId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid AttendeeId { get; set; }
        }

        public class Response
        {
            public AttendeeDto Attendee { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepository _repository;
            
			public Handler(IRepository repository) => _repository = repository;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
			     => Task.FromResult(new Response()
                {
                    Attendee = AttendeeDto.FromAttendee(_repository.Query<Attendee>().Single(x => x.AttendeeId == request.AttendeeId))
                });
        }
    }
}
