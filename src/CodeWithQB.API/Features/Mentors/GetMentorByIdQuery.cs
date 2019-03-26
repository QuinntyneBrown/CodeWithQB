using CodeWithQB.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Mentors
{
    public class GetMentorByIdQuery
    {
        public class Request : IRequest<Response> {
            public Guid MentorId { get; set; }
        }

        public class Response
        {
            public MentorDto Mentor { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Mentor = (await _context.Mentors.FindAsync(request.MentorId)).ToDto()
                };
        }
    }
}
