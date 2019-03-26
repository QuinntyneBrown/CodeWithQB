using CodeWithQB.Core.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Mentors
{
    public class RemoveMentorCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.MentorId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid MentorId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var mentor = await _context.Mentors.FindAsync(request.MentorId);

                _context.Mentors.Remove(mentor);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
