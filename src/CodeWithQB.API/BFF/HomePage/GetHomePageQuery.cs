using CodeWithQB.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
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

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new Response
                {
                    HomePage = new HomePageViewModel
                    {
                        FullName = "Quinntyne Brown",
                        Title = "Architect and Senior Software Engineer",
                        ImageUrl = "https://avatars0.githubusercontent.com/u/1749159?s=400&u=b36e138431ef4f0a383e51eef90248ad07066b28&v=4"
                    }
                });
            }
        }
    }
}
