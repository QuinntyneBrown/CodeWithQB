using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace CodeWithQB.API.Features.Addresses
{
    public class UpdateAddressCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Address.AddressId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public AddressDto Address { get; set; }
        }

        public class Response
        {            
            public Guid AddressId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var address = _eventStore.Load<Address>(request.Address.AddressId);

                address.ChangeName(request.Address.Name);

                _eventStore.Save(address);

                return Task.FromResult(new Response() { AddressId = request.Address.AddressId }); 
            }
        }
    }
}
