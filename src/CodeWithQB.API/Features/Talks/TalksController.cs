using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.Talks
{
    [ApiController]
    [Route("api/talks")]
    [Authorize]
    public class TalksController
    {
        private readonly IMediator _mediator;
        
        public TalksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<GetTalksQuery.Response> Get()
        {
            return await _mediator.Send(new GetTalksQuery.Request());
        }
    }
}
