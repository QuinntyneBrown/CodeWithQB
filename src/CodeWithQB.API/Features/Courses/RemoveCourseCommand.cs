using CodeWithQB.Core.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Courses
{
    public class RemoveCourseCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.CourseId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid CourseId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var course = await _context.Courses.FindAsync(request.CourseId);

                _context.Courses.Remove(course);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
