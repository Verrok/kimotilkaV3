using System.Threading.Tasks;
using KimotilkaV3.Models;
using Microsoft.AspNetCore.Mvc;

namespace KimotilkaV3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {




        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Shorten([FromBody] RequestUrl requestUrl)
        {

            return Ok();
        }
    }
}