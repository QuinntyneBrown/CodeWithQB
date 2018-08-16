using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Dashboards
{
    [Authorize]
    [ApiController]
    [Route("api/dashboards")]
    public class DashboardsController
    {
        private readonly IMediator _mediator;

        public DashboardsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateDashboardCommand.Response>> Create(CreateDashboardCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateDashboardCommand.Response>> Update([FromBody]UpdateDashboardCommand.Request request)
            => await _mediator.Send(request);

        [HttpGet("default")]
        public async Task<ActionResult<GetDashboardByDefaultQuery.Response>> GetDefault()
            => await _mediator.Send(new GetDashboardByDefaultQuery.Request());

        [HttpDelete("{dashboardId}")]
        public async Task Remove([FromRoute]RemoveDashboardCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{dashboardId}")]
        public async Task<ActionResult<GetDashboardByIdQuery.Response>> GetById([FromRoute]GetDashboardByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetDashboardsQuery.Response>> Get()
            => await _mediator.Send(new GetDashboardsQuery.Request());
    }
}
