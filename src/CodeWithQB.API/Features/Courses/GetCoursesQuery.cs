using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Courses
{
    public class GetCoursesQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<CourseDto> Courses { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                =>  new Response()
                {
                    Courses = await _context.Courses.Select(x => x.ToDto()).ToArrayAsync()
                };
        }
    }
}
