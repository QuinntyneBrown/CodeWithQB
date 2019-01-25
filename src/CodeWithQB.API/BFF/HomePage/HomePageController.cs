using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CodeWithQB.API.BFF.HomePage
{
    [ApiController]
    [Route("api/homepage")]
    public class HomePageController
    {
        private readonly IMediator _mediator;

        public HomePageController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(HomePageViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetHomePageViewModel.Response>> Get() 
            => await Task.FromResult(new GetHomePageViewModel.Response {
                HomePage = new HomePageViewModel
                {
                    FullName = "Quinntyne Brown",
                    Title = "Architect and Senior Software Engineer",
                    ImageUrl = "https://avatars0.githubusercontent.com/u/1749159?s=400&u=b36e138431ef4f0a383e51eef90248ad07066b28&v=4"
                }
            });
    }
}
