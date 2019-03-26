using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Customers
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
        public async Task<ActionResult<GetCustomersQuery.Response>> Get(CancellationToken cancellationToken)
            => await _meditator.Send(new GetCustomersQuery.Request(), cancellationToken);

        [HttpGet("{customerId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCustomerByIdQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCustomerByIdQuery.Response>> GetById(GetCustomerByIdQuery.Request request, CancellationToken cancellationToken)
            => await _meditator.Send(request, cancellationToken);

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpsertCustomerCommand.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpsertCustomerCommand.Response>> Upsert(UpsertCustomerCommand.Request request, CancellationToken cancellationToken)
            => await _meditator.Send(request, cancellationToken);

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> Upsert([FromQuery]RemoveCustomerCommand.Request request, CancellationToken cancellationToken)
            => await _meditator.Send(request, cancellationToken);
    }
}
