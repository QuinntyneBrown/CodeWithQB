using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.DigitalAssets
{
    [ApiController]
    [Route("api/digitalassets")]
    public class DigitalAssetsController
    {
        public async Task<ActionResult<string>> Get() {
            return await Task.FromResult("Digital Assets Controller");
        }
    }
}
