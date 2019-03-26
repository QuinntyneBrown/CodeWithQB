using CodeWithQB.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Mentors
{
    public class GetMentorsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<MentorDto> Mentors { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                =>  new Response()
                {
                    Mentors = await _context.Mentors.Select(x => x.ToDto()).ToArrayAsync()
                };
        }
    }
}
