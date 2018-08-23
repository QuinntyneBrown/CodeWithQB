using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Addresses
{
    public class GetAddressByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.AddressId).NotEqual(default(Guid));
            }
        }

        public class Request : IRequest<Response> {
            public Guid AddressId { get; set; }
        }

        public class Response
        {
            public AddressDto Address { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            
			public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
			     => Task.FromResult(new Response()
                {
                    Address = AddressDto.FromAddress(_eventStore.Query<Address>(request.AddressId))
                });
        }
    }
}
