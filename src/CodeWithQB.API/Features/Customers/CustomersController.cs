using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Customers
{
    [Authorize]
    [ApiController]
    [Route("api/customers")]
    public class CustomersController
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateCustomerCommand.Response>> Create(CreateCustomerCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateCustomerCommand.Response>> Update([FromBody]UpdateCustomerCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{customerId}")]
        public async Task Remove([FromRoute]RemoveCustomerCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{customerId}")]
        public async Task<ActionResult<GetCustomerByIdQuery.Response>> GetById([FromRoute]GetCustomerByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetCustomersQuery.Response>> Get()
            => await _mediator.Send(new GetCustomersQuery.Request());
    }
}
