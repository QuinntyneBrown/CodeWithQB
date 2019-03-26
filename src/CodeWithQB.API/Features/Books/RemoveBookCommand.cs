using CodeWithQB.Core.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Books
{
    public class RemoveBookCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.BookId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid BookId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var book = await _context.Books.FindAsync(request.BookId);

                _context.Books.Remove(book);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
