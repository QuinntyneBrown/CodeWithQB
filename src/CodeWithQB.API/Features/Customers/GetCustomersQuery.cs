using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Customers
{
    public class GetCustomersQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<CustomerDto> Customers { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepository _repository;

            public Handler(IRepository repository) => _repository = repository;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => Task.FromResult(new Response()
                {
                    Customers = _repository.Query<Customer>().Select(x => CustomerDto.FromCustomer(x)).ToList()
                });
        }
    }
}
