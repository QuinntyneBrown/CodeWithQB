using CodeWithQB.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Talks
{
    public class GetTalksQuery
    {
        public class Request: IRequest<Response>
        {

        }

        public class Response
        {
            public IEnumerable<TalkDto> Talks { get; set; }
        }

        public class Handler: IRequestHandler<Request,Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
