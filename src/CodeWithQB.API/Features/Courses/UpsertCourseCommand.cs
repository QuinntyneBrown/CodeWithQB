using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Courses
{
    public class UpsertCourseCommand
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Course.CourseId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public CourseDto Course { get; set; }
        }

        public class Response
        {
            public Guid CourseId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var course = await _context.Courses.FindAsync(request.Course.CourseId);

                if (course == null) {
                    course = new Course();
                    _context.Courses.Add(course);
                }

                course.Name = request.Course.Name;
                course.Description = request.Course.Description;
                course.Abstract = request.Course.Abstract;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { CourseId = course.CourseId };
            }
        }
    }
}
