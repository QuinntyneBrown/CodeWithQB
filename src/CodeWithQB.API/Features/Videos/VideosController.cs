using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Videos
{
    [Authorize]
    [ApiController]
    [Route("api/videos")]
    public class VideosController
    {
        private readonly IMediator _meditator;

        public VideosController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetVideosQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetVideosQuery.Response>> Get()
            => await _meditator.Send(new GetVideosQuery.Request());

        [HttpGet("{videoId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetVideoByIdQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetVideoByIdQuery.Response>> GetById(GetVideoByIdQuery.Request request)
            => await _meditator.Send(request);

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpsertVideoCommand.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpsertVideoCommand.Response>> Upsert(UpsertVideoCommand.Request request)
            => await _meditator.Send(request);

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> Remove([FromQuery]RemoveVideoCommand.Request request)
            => await _meditator.Send(request);
    }
}
