using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    [Authorize]
    [ApiController]
    [Route("api/shoppingCartItems")]
    public class ShoppingCartItemsController
    {
        private readonly IMediator _mediator;

        public ShoppingCartItemsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateShoppingCartItemCommand.Response>> Create(CreateShoppingCartItemCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateShoppingCartItemCommand.Response>> Update([FromBody]UpdateShoppingCartItemCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{shoppingCartItemId}")]
        public async Task Remove([FromRoute]RemoveShoppingCartItemCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{shoppingCartItemId}")]
        public async Task<ActionResult<GetShoppingCartItemByIdQuery.Response>> GetById([FromRoute]GetShoppingCartItemByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetShoppingCartItemsQuery.Response>> Get()
            => await _mediator.Send(new GetShoppingCartItemsQuery.Request());
    }
}
