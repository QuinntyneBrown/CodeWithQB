using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Events
{
    [Authorize]
    [ApiController]
    [Route("api/events")]
    public class EventsController
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateEventCommand.Response>> Create(CreateEventCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateEventCommand.Response>> Update([FromBody]UpdateEventCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{eventId}")]
        public async Task Remove([FromRoute]RemoveEventCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{eventId}")]
        public async Task<ActionResult<GetEventByIdQuery.Response>> GetById([FromRoute]GetEventByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetEventsQuery.Response>> Get()
            => await _mediator.Send(new GetEventsQuery.Request());
    }
}
