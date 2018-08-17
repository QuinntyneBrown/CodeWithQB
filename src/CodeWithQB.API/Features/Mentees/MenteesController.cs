using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Mentees
{
    [Authorize]
    [ApiController]
    [Route("api/mentees")]
    public class MenteesController
    {
        private readonly IMediator _mediator;

        public MenteesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateMenteeCommand.Response>> Create(CreateMenteeCommand.Request request)
            => await _mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<UpdateMenteeCommand.Response>> Update([FromBody]UpdateMenteeCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{menteeId}")]
        public async Task Remove([FromRoute]RemoveMenteeCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{menteeId}")]
        public async Task<ActionResult<GetMenteeByIdQuery.Response>> GetById([FromRoute]GetMenteeByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetMenteesQuery.Response>> Get()
            => await _mediator.Send(new GetMenteesQuery.Request());
    }
}
