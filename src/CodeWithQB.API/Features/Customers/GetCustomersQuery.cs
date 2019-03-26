using CodeWithQB.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Customers
{
    public class GetCustomersQuery
    {
        public class Request: IRequest<Response> { }

        public class Response
        {
            public CustomerDto[] Customers { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                =>  new Response()
                {
                    Customers = await _context.Customers.Select(x => CustomerDto.FromCustomer(x)).ToArrayAsync()
                };
        }
    }
}
