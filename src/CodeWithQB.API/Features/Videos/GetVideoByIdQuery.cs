using CodeWithQB.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Videos
{
    public class GetVideoByIdQuery
    {
        public class Request : IRequest<Response> {
            public Guid VideoId { get; set; }
        }

        public class Response
        {
            public VideoDto Video { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Video = (await _context.Videos.FindAsync(request.VideoId)).ToDto()
                };
        }
    }
}
