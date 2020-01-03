using System;
using System.Threading.Tasks;
using KimotilkaV3.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace KimotilkaV3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {

        private readonly ILogger _logger;
        
        public UrlController(ILogger logger)
        {
            _logger = logger;
        }     


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Shorten([FromBody] RequestUrl obj)
        {
            _logger.Information("Received a request");
            _logger.Information("Request body: {@obj}", obj);
            
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
                            } while (await DbMethods.CheckHash(hash));
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
                            end = null;
                        }
                    }

                    _logger.Information("Creating url with hash: {hash} \n start date: {sd} \n end date: {ed}", hash, start, end);
                    
                    await DbMethods.CreateUrl(hash, obj.Url, start, end);
                    
                    return new JsonResult(new {hash = hash});

                    
                    
                }
                _logger.Warning("Server received invalid url: {url}", obj.Url);
                return BadRequest("Url is invalid");
            }
            
            _logger.Error("Server didn't receive url");
            return BadRequest("No url");
        }
    }
}