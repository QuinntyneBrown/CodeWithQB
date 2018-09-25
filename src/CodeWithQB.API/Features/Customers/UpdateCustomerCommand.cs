using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace CodeWithQB.API.Features.Customers
{
    public class UpdateCustomerCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Customer.CustomerId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public CustomerDto Customer { get; set; }
        }

        public class Response
        {			
            public Guid CustomerId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {                
				var customer = _eventStore.Load<Customer>(request.Customer.CustomerId);

                _eventStore.Save(customer);

                return Task.FromResult(new Response() { CustomerId = request.Customer.CustomerId }); 
            }
        }
    }
}
