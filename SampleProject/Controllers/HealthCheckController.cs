using Microsoft.AspNetCore.Mvc;

namespace SampleProject.API.Controllers
{
    public class HealthCheckController : Controller
    {
        [HttpGet, Route("Health/Status")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
