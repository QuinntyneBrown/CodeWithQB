using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Attendees
{
    [Authorize]
    [ApiController]
    [Route("api/attendees")]
    public class AttendeesController
    {
        private readonly IMediator _mediator;

        public AttendeesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateAttendeeCommand.Response>> Create(CreateAttendeeCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateAttendeeCommand.Response>> Update([FromBody]UpdateAttendeeCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{attendeeId}")]
        public async Task Remove([FromRoute]RemoveAttendeeCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{attendeeId}")]
        public async Task<ActionResult<GetAttendeeByIdQuery.Response>> GetById([FromRoute]GetAttendeeByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetAttendeesQuery.Response>> Get()
            => await _mediator.Send(new GetAttendeesQuery.Request());
    }
}
