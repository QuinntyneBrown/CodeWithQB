using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.ShoppingCarts
{
    [Authorize]
    [ApiController]
    [Route("api/shoppingCarts")]
    public class ShoppingCartsController
    {
        private readonly IMediator _mediator;

        public ShoppingCartsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateShoppingCartCommand.Response>> Create(CreateShoppingCartCommand.Request request)
            => await _mediator.Send(request);

        [HttpPost]
        [Route("checkout")]
        public async Task<ActionResult<CheckoutCommand.Response>> Checkout()
            => await _mediator.Send(new CheckoutCommand.Request());

        [HttpPut]
        public async Task<ActionResult<UpdateShoppingCartCommand.Response>> Update([FromBody]UpdateShoppingCartCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{shoppingCartId}")]
        public async Task Remove([FromRoute]RemoveShoppingCartCommand.Request request)
            => await _mediator.Send(request);

        [HttpGet("current")]
        public async Task<ActionResult<GetCurrentShoppingCartCommand.Response>> Current()
            => await _mediator.Send(new GetCurrentShoppingCartCommand.Request());

        [HttpGet("{shoppingCartId}")]
        public async Task<ActionResult<GetShoppingCartByIdQuery.Response>> GetById([FromRoute]GetShoppingCartByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetShoppingCartsQuery.Response>> Get()
            => await _mediator.Send(new GetShoppingCartsQuery.Request());
        

    }
}
