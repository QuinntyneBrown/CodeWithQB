using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Bookings
{
    [Authorize]
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateBookingCommand.Response>> Create(CreateBookingCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateBookingCommand.Response>> Update([FromBody]UpdateBookingCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{bookingId}")]
        public async Task Remove([FromRoute]RemoveBookingCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{bookingId}")]
        public async Task<ActionResult<GetBookingByIdQuery.Response>> GetById([FromRoute]GetBookingByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetBookingsQuery.Response>> Get()
            => await _mediator.Send(new GetBookingsQuery.Request());
    }
}
