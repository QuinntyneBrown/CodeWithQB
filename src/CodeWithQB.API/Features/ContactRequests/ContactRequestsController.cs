using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.ContactRequests
{
    [Authorize]
    [ApiController]
    [Route("api/contactRequests")]
    public class ContactRequestsController
    {
        private readonly IMediator _meditator;

        public ContactRequestsController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetContactRequestsQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetContactRequestsQuery.Response>> Get()
            => await _meditator.Send(new GetContactRequestsQuery.Request());

        [HttpGet("{contactRequestId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetContactRequestByIdQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetContactRequestByIdQuery.Response>> GetById(GetContactRequestByIdQuery.Request request)
            => await _meditator.Send(request);

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpsertContactRequestCommand.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpsertContactRequestCommand.Response>> Upsert(UpsertContactRequestCommand.Request request)
            => await _meditator.Send(request);

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> Upsert([FromQuery]RemoveContactRequestCommand.Request request)
            => await _meditator.Send(request);
    }
}
