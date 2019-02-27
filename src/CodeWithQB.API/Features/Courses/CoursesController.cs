using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Courses
{
    [Authorize]
    [ApiController]
    [Route("api/courses")]
    public class CoursesController
    {
        private readonly IMediator _meditator;

        public CoursesController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCoursesQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCoursesQuery.Response>> Get()
            => await _meditator.Send(new GetCoursesQuery.Request());

        [HttpGet("{courseId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetCourseByIdQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetCourseByIdQuery.Response>> GetById(GetCourseByIdQuery.Request request)
            => await _meditator.Send(request);

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpsertCourseCommand.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpsertCourseCommand.Response>> Upsert(UpsertCourseCommand.Request request)
            => await _meditator.Send(request);

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> Remove([FromQuery]RemoveCourseCommand.Request request)
            => await _meditator.Send(request);
    }
}
