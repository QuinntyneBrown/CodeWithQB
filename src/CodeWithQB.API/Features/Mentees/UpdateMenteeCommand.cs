using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace CodeWithQB.API.Features.Mentees
{
    public class UpdateMenteeCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Mentee.MenteeId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public MenteeDto Mentee { get; set; }
        }

        public class Response
        {			
            public Guid MenteeId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var mentee = _eventStore.Query<Mentee>().Single(x => x.MenteeId == request.Mentee.MenteeId);

                mentee.ChangeName(request.Mentee.FirstName, request.Mentee.LastName);

                _eventStore.Save(mentee);

                return Task.FromResult(new Response() { MenteeId = request.Mentee.MenteeId }); 
            }
        }
    }
}
