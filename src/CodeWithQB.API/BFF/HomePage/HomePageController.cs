using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace CodeWithQB.API.BFF.HomePage
{
    [ApiController]
    [Route("api/homepage")]
    public class HomePageController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HomePageController> _logger;

        public HomePageController(ILogger<HomePageController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(HomePageViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetHomePageQuery.Response>> Get() 
            => await _mediator.Send(new GetHomePageQuery.Request());
    }
}
