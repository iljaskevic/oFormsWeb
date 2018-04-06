using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace oFormsWeb.Controllers
{
    [Route("health")]
    [EnableCors("AllowAllOrigins")]
    public class HealthController : Controller
    {
        private readonly ILogger _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
