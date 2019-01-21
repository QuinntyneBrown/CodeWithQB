using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Products
{
    [Authorize]
    [ApiController]
    [Route("api/products")]
    public class ProductsController
    {
        private readonly IMediator _meditator;

        public ProductsController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProductsQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProductsQuery.Response>> Get()
            => await _meditator.Send(new GetProductsQuery.Request());

        [HttpGet("{productId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetProductByIdQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetProductByIdQuery.Response>> GetById(GetProductByIdQuery.Request request)
            => await _meditator.Send(request);

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpsertProductCommand.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpsertProductCommand.Response>> Upsert(UpsertProductCommand.Request request)
            => await _meditator.Send(request);

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> Remove([FromQuery]RemoveProductCommand.Request request)
            => await _meditator.Send(request);
    }
}
