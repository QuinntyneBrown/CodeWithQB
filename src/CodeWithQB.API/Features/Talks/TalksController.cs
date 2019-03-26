using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Talks
{
    [ApiController]
    [Route("api/talks")]    
    public class TalksController: ControllerBase
    {
        private readonly IMediator _mediator;
        
        public TalksController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        [Route("")]
        public async Task<GetTalksQuery.Response> Get()
            => await _mediator.Send(new GetTalksQuery.Request());

        [HttpPut]
        [Route("")]
        public async Task<UpsertTalkCommand.Response> Get([FromBody]UpsertTalkCommand.Request request)
            => await _mediator.Send(request);

    }
}
