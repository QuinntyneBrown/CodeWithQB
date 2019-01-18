using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Locations
{
    [Authorize]
    [ApiController]
    [Route("api/locations")]
    public class LocationsController
    {
        private readonly IMediator _meditator;

        public LocationsController(IMediator mediator) => _meditator = mediator;

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetLocationsQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetLocationsQuery.Response>> Get()
            => await _meditator.Send(new GetLocationsQuery.Request());

        [HttpGet("{locationId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetLocationByIdQuery.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetLocationByIdQuery.Response>> GetById(GetLocationByIdQuery.Request request)
            => await _meditator.Send(request);

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(UpsertLocationCommand.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UpsertLocationCommand.Response>> Upsert(UpsertLocationCommand.Request request)
            => await _meditator.Send(request);

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> Upsert([FromQuery]RemoveLocationCommand.Request request)
            => await _meditator.Send(request);
    }
}
