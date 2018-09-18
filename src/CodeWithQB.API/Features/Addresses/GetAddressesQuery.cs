using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Addresses
{
    public class GetAddressesQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<AddressDto> Addresses { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRepository _repository;

            public Handler(IRepository repository) => _repository = repository;

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => Task.FromResult(new Response()
                {
                    Addresses = _repository.Query<Address>().Select(x => AddressDto.FromAddress(x)).ToList()
                });
        }
    }
}
