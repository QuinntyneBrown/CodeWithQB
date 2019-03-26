using CodeWithQB.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Books
{
    public class GetBookByIdQuery
    {
        public class Request : IRequest<Response> {
            public Guid BookId { get; set; }
        }

        public class Response
        {
            public BookDto Book { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Book = (await _context.Books.FindAsync(request.BookId)).ToDto()
                };
        }
    }
}
