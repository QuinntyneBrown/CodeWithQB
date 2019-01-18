using CodeWithQB.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.ContactRequests
{
    public class GetContactRequestByIdQuery
    {
        public class Request : IRequest<Response> {
            public Guid ContactRequestId { get; set; }
        }

        public class Response
        {
            public ContactRequestDto ContactRequest { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    ContactRequest = ContactRequestDto.FromContactRequest(await _context.ContactRequests.FindAsync(request.ContactRequestId))
                };
        }
    }
}
