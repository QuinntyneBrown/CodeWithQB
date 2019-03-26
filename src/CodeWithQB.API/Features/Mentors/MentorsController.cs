using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Mentors
{
    [ApiController]
    [Route("api/mentors")]
    public class MentorsController: ControllerBase
    {
        private readonly IMediator _meditator;

        public MentorsController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetMentorsQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetMentorsQuery.Response>> Get()
            => await _meditator.Send(new GetMentorsQuery.Request());

        [HttpGet("{mentorId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetMentorByIdQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetMentorByIdQuery.Response>> GetById(GetMentorByIdQuery.Request request)
            => await _meditator.Send(request);

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpsertMentorCommand.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpsertMentorCommand.Response>> Upsert(UpsertMentorCommand.Request request)
            => await _meditator.Send(request);

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> Upsert([FromQuery]RemoveMentorCommand.Request request)
            => await _meditator.Send(request);
    }
}
