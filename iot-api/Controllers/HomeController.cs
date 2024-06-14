using Microsoft.AspNetCore.Mvc;

namespace iot_api.Controllers
{
    [ApiController]
    [Route("")]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("OK!");
        }
    }
}
