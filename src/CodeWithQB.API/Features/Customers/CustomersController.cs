using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Customers
{
    [Authorize]
    [ApiController]
    [Route("api/customers")]
    public class CustomersController
    {
        private readonly IMediator _meditator;

        public CustomersController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCustomersQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCustomersQuery.Response>> Get()
            => await _meditator.Send(new GetCustomersQuery.Request());

        [HttpGet("{customerId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCustomerByIdQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCustomerByIdQuery.Response>> GetById(GetCustomerByIdQuery.Request request)
            => await _meditator.Send(request);

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpsertCustomerCommand.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpsertCustomerCommand.Response>> Upsert(UpsertCustomerCommand.Request request)
            => await _meditator.Send(request);

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> Upsert([FromQuery]RemoveCustomerCommand.Request request)
            => await _meditator.Send(request);
    }
}
