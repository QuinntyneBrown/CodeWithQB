using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Mentors
{
    public class UpsertMentorCommand
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Mentor.MentorId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public MentorDto Mentor { get; set; }
        }

        public class Response
        {
            public Guid MentorId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var mentor = await _context.Mentors.FindAsync(request.Mentor.MentorId);

                if (mentor == null) {
                    mentor = new Mentor();
                    _context.Mentors.Add(mentor);
                }

                mentor.FullName = request.Mentor.FullName;
                mentor.Title = request.Mentor.Title;
                mentor.ImageUrl = request.Mentor.ImageUrl;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { MentorId = mentor.MentorId };
            }
        }
    }
}
