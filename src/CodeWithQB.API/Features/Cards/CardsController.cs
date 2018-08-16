using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Cards
{
    [Authorize]
    [ApiController]
    [Route("api/cards")]
    public class CardsController
    {
        private readonly IMediator _mediator;

        public CardsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateCardCommand.Response>> Create(CreateCardCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateCardCommand.Response>> Update([FromBody]UpdateCardCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{cardId}")]
        public async Task Remove([FromRoute]RemoveCardCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{cardId}")]
        public async Task<ActionResult<GetCardByIdQuery.Response>> GetById([FromRoute]GetCardByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetCardsQuery.Response>> Get()
            => await _mediator.Send(new GetCardsQuery.Request());
    }
}
