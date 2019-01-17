using MediatR;

namespace CodeWithQB.API.Features.Addresses
{
    public class AddressesController
    {
        private readonly IMediator _mediator;

        public AddressesController(IMediator mediator)
            => _mediator = mediator;
        
    }
}
