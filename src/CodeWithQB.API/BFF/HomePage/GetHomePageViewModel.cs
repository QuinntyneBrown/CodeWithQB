using CodeWithQB.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.BFF.HomePage
{
    public class GetHomePageViewModel
    {
        public class Request: IRequest<Response> { }

        public class Response
        {
            public HomePageViewModel HomePage { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
