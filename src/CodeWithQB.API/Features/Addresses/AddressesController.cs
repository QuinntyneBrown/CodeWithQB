using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Addresses
{
    [Authorize]
    [ApiController]
    [Route("api/addresses")]
    public class AddressesController
    {
        private readonly IMediator _mediator;

        public AddressesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateAddressCommand.Response>> Create(CreateAddressCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateAddressCommand.Response>> Update([FromBody]UpdateAddressCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{addressId}")]
        public async Task Remove([FromRoute]RemoveAddressCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{addressId}")]
        public async Task<ActionResult<GetAddressByIdQuery.Response>> GetById([FromRoute]GetAddressByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetAddressesQuery.Response>> Get()
            => await _mediator.Send(new GetAddressesQuery.Request());
    }
}
