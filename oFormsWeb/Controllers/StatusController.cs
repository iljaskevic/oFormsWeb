using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace oFormsWeb.Controllers
{
    [Route("status")]
    [EnableCors("AllowAllOrigins")]
    public class StatusController : Controller
    {
        private readonly ILogger _logger;

        public StatusController(ILogger<StatusController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("health")]
        public IActionResult GetHealth()
        {
            return Ok();
        }
    }
}
