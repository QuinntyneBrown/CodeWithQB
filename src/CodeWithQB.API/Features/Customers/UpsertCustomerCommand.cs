using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Customers
{
    public class UpsertCustomerCommand
    {
        public class Request : IRequest<Response> {
            public CustomerDto Customer { get; set; }
        }

        public class Response
        {
            public Guid CustomerId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var customer = await _context.Customers.FindAsync(request.Customer.CustomerId);

                if (customer == null) {
                    customer = new Customer();
                    _context.Customers.Add(customer);
                }

                customer.Name = request.Customer.Name;
                customer.IsLive = request.Customer.IsLive;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { CustomerId = customer.CustomerId };
            }
        }
    }
}
