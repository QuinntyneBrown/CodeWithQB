using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Tags
{
    [Authorize]
    [ApiController]
    [Route("api/tags")]
    public class TagsController
    {
        private readonly IMediator _meditator;

        public TagsController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTagsQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTagsQuery.Response>> Get()
            => await _meditator.Send(new GetTagsQuery.Request());

        [HttpGet("{tagId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetTagByIdQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetTagByIdQuery.Response>> GetById(GetTagByIdQuery.Request request)
            => await _meditator.Send(request);

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpsertTagCommand.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpsertTagCommand.Response>> Upsert(UpsertTagCommand.Request request)
            => await _meditator.Send(request);

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> Upsert([FromQuery]RemoveTagCommand.Request request)
            => await _meditator.Send(request);
    }
}
