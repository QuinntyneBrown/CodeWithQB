using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeWithQB.API.Features.DigitalAssets
{
    [Authorize]
    [ApiController]
    [Route("api/digitalAssets")]
    public class DigitalAssetsController
    {
        private readonly IMediator _mediator;

        public DigitalAssetsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveDigitalAssetCommand.Response>> Save(SaveDigitalAssetCommand.Request request)
            => await _mediator.Send(request);

        [HttpGet("range")]
        public async Task<ActionResult<GetDigitalAssetsByIdsQuery.Response>> GetByIds([FromQuery]GetDigitalAssetsByIdsQuery.Request request)
            => await _mediator.Send(request);

        [HttpDelete("{digitalAssetId}")]
        public async Task Remove(RemoveDigitalAssetCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{digitalAssetId}")]
        public async Task<ActionResult<GetDigitalAssetByIdQuery.Response>> GetById([FromRoute]GetDigitalAssetByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<ActionResult<UploadDigitalAssetCommand.Response>> Save()
            => await _mediator.Send(new UploadDigitalAssetCommand.Request());
        
        [AllowAnonymous]
        [HttpGet("serve/{digitalAssetId}")]
        public async Task<IActionResult> Serve([FromRoute]GetDigitalAssetByIdQuery.Request request)
        {
            var response = await _mediator.Send(request);
            return new FileContentResult(response.DigitalAsset.Bytes, response.DigitalAsset.ContentType);            
        }

        [HttpGet]
        public async Task<ActionResult<GetDigitalAssetsQuery.Response>> Get()
            => await _mediator.Send(new GetDigitalAssetsQuery.Request());
    }
}
