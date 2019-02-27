using CodeWithQB.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Courses
{
    public class GetCourseByIdQuery
    {
        public class Request : IRequest<Response> {
            public Guid CourseId { get; set; }
        }

        public class Response
        {
            public CourseDto Course { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Course = (await _context.Courses.FindAsync(request.CourseId)).ToDto()
                };
        }
    }
}
