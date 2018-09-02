using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Mentees
{
    public class GetMenteeByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.MenteeId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid MenteeId { get; set; }
        }

        public class Response
        {
            public MenteeDto Mentee { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
                 => Task.FromResult(new Response()
                {
                    Mentee = MenteeDto.FromMentee(_eventStore.Query<Mentee>().Single(x => x.MenteeId == request.MenteeId))
                });
        }
    }
}
