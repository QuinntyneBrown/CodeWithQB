using CodeWithQB.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Tags
{
    public class GetTagByIdQuery
    {
        public class Request : IRequest<Response> {
            public Guid TagId { get; set; }
        }

        public class Response
        {
            public TagDto Tag { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Tag = (await _context.Tags.FindAsync(request.TagId)).ToDto()
                };
        }
    }
}
