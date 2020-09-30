using Microsoft.AspNetCore.Mvc;

namespace LoanManager.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Application is runing!");
        }
    }
}
