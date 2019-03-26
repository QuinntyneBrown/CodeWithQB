using CodeWithQB.Core.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Products
{
    public class RemoveProductCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProductId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid ProductId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.ProductId);

                _context.Products.Remove(product);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
