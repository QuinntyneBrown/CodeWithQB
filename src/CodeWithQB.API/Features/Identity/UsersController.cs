using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Identity
{
    [ApiController]
    [Route("api/users")]
    public class UsersController
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpPost("token")]
        public async Task<ActionResult<AuthenticateCommand.Response>> SignIn(AuthenticateCommand.Request request)
            => await _mediator.Send(request);
    }
}
