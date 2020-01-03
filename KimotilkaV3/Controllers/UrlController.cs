using System;
using System.Threading.Tasks;
using KimotilkaV3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace KimotilkaV3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {

        private readonly ILogger<UrlController> _logger;
        
        public UrlController(ILogger<UrlController> logger)
        {
            _logger = logger;
        }     


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Shorten([FromBody] RequestUrl obj)
        {
            _logger.LogInformation("Received a request");
            _logger.LogInformation("Request body: {@obj}", obj);
            
            if (obj.Url != null)
            {
                string url = obj.Url;
                if (UrlHelper.IsUrlValid(url))
                {
                    string hash = "";
                    DateTime start = DateTime.Today;
                    DateTime? end = DateTime.Today;
                    
                    if (obj.AdvancedSettings != null)
                    {
                        if (obj.AdvancedSettings.Name == null)
                        {
                            do
                            {
                                hash = UrlHelper.HashUrl(url);
                            } while (await DbMethods.IsHashExists(hash));
                        }
                        else
                        {
                            hash = obj.AdvancedSettings.Name;
                        }

                        if (obj.AdvancedSettings.StartDate != null)
                        {
                            start = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                            start = start.AddMilliseconds(obj.AdvancedSettings.StartDate.Value).ToLocalTime();
                        }
                        
                        if (obj.AdvancedSettings.ExpireDate != null)
                        {
                            end = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                            end = end.Value.AddMilliseconds(obj.AdvancedSettings.ExpireDate.Value).ToLocalTime();
                        }
                        else
                        {
                            end = end.Value.AddYears(5000);
                        }
                    }
                    else
                    {
                        do
                        {
                            hash = UrlHelper.HashUrl(url);
                        } while (await DbMethods.IsHashExists(hash));
                        end = end.Value.AddYears(5000);

                    }

                    _logger.LogInformation("Creating url with hash: {hash}, start date: {sd}, end date: {ed}", hash, start, end);
                    
                    await DbMethods.CreateUrl(hash, obj.Url, start, end);
                    return new JsonResult(new {hash = hash});

                }
                _logger.LogWarning("Server received invalid url: {url}", obj.Url);
                return BadRequest("Url is invalid");
            }
            
            _logger.LogWarning("Server didn't receive url");
            return BadRequest("No url");
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Get([FromQuery] string hash)
        {
            if (await DbMethods.IsHashExists(hash))
            {
                Url url = await DbMethods.GetUrlByHash(hash);
                DateTime now = DateTime.Now;
                if (url.ExpirationDate <= now)
                {
                    await DbMethods.Deactivate(hash);
                    return new JsonResult(new
                    {
                        message = "Url expired"
                    });
                }
                if (url.CreateDate <= now)
                {
                    return new JsonResult(new
                    {
                        Url = url.Link
                    });
                }
                
            }
            return BadRequest("Url with this hash doesnt exist");
        }
        
        
    }
}