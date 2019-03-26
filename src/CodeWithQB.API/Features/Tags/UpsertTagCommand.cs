using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Tags
{
    public class UpsertTagCommand
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Tag.TagId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public TagDto Tag { get; set; }
        }

        public class Response
        {
            public Guid TagId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var tag = await _context.Tags.FindAsync(request.Tag.TagId);

                if (tag == null) {
                    tag = new Tag();
                    _context.Tags.Add(tag);
                }

                tag.Name = request.Tag.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { TagId = tag.TagId };
            }
        }
    }
}
