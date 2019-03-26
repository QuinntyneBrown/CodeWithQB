using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Videos
{
    public class GetVideosQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<VideoDto> Videos { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                =>  new Response()
                {
                    Videos = await _context.Videos.Select(x => x.ToDto()).ToArrayAsync()
                };
        }
    }
}
