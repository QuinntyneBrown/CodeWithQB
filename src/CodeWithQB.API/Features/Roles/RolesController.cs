using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Roles
{
    [Authorize]
    [ApiController]
    [Route("api/roles")]
    public class RolesController
    {
        private readonly IMediator _mediator;
        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }
            
        [HttpPost]
        public async Task<ActionResult<CreateRoleCommand.Response>> Create(CreateRoleCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateRoleCommand.Response>> Update([FromBody]UpdateRoleCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{roleId}")]
        public async Task Remove([FromRoute]RemoveRoleCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{roleId}")]
        public async Task<ActionResult<GetRoleByIdQuery.Response>> GetById([FromRoute]GetRoleByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetRolesQuery.Response>> Get()
            => await _mediator.Send(new GetRolesQuery.Request());
    }
}
