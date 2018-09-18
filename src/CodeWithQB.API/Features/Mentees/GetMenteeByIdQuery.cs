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
            private readonly IRepository _repository;

            public Handler(IRepository repository) => _repository = repository;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
                 => Task.FromResult(new Response()
                {
                    Mentee = MenteeDto.FromMentee(_repository.Query<Mentee>().Single(x => x.MenteeId == request.MenteeId))
                });
        }
    }
}
