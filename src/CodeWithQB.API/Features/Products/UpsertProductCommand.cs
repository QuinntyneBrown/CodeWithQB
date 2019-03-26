using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Products
{
    public class UpsertProductCommand
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Product.ProductId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ProductDto Product { get; set; }
        }

        public class Response
        {
            public Guid ProductId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var product = await _context.Products.FindAsync(request.Product.ProductId);

                if (product == null) {
                    product = new Product();
                    _context.Products.Add(product);
                }

                product.Name = request.Product.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ProductId = product.ProductId };
            }
        }
    }
}
