using CodeWithQB.API.Features.Courses;
using CodeWithQB.API.Features.Videos;
using CodeWithQB.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.BFF.HomePage
{
    public class GetHomePageQuery
    {
        public class Request: IRequest<Response> { }

        public class Response
        {
            public HomePageViewModel HomePage { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly ILogger<Handler> _logger;

            public Handler(IAppDbContext context, ILogger<Handler> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {                
                return new Response
                {
                    HomePage = new HomePageViewModel
                    {
                        FullName = "Quinntyne Brown",
                        Title = "Architect and Senior Software Engineer",
                        ImageUrl = "https://avatars0.githubusercontent.com/u/1749159?s=400&u=b36e138431ef4f0a383e51eef90248ad07066b28&v=4",
                        Courses = await _context.Courses.Select(x => x.ToDto()).ToArrayAsync(),
                        Videos = await _context.Videos.Select(x => x.ToDto()).ToArrayAsync()
                    }
                };
            }
        }
    }
}
