using MediatR;
using Microsoft.Extensions.Logging;

namespace CodeWithQB.Api.Features.Addresses
{
    public class AddressesController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AddressesController> _logger;

        public AddressesController(IMediator mediator, ILogger<AddressesController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }
        
    }
}
