using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Books
{
    [Authorize]
    [ApiController]
    [Route("api/books")]
    public class BooksController
    {
        private readonly IMediator _meditator;

        public BooksController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetBooksQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetBooksQuery.Response>> Get()
            => await _meditator.Send(new GetBooksQuery.Request());

        [HttpGet("{bookId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetBookByIdQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetBookByIdQuery.Response>> GetById(GetBookByIdQuery.Request request)
            => await _meditator.Send(request);

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpsertBookCommand.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpsertBookCommand.Response>> Upsert(UpsertBookCommand.Request request)
            => await _meditator.Send(request);

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> Remove([FromQuery]RemoveBookCommand.Request request)
            => await _meditator.Send(request);
    }
}
