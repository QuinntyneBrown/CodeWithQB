using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Customers
{
    public class GetCustomerByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.CustomerId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid CustomerId { get; set; }
        }

        public class Response
        {
            public CustomerDto Customer { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepository _repository;
            
			public Handler(IRepository repository) => _repository = repository;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
			     => Task.FromResult(new Response()
                {
                    Customer = CustomerDto.FromCustomer(_repository.Query<Customer>().Single(x => x.CustomerId == request.CustomerId))
                });
        }
    }
}
