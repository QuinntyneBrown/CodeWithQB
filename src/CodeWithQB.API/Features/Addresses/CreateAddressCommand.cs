using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace CodeWithQB.API.Features.Addresses
{
    public class CreateAddressCommand
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
                var address = new Address(request.Address.Name);

                _eventStore.Save(address);
                
                return Task.FromResult(new Response() { AddressId = address.AddressId });
            }
        }
    }
}
