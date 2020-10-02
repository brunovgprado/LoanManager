using LoanManager.Api.Models;
using LoanManager.Application.Interfaces.AppServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LoanManager.Api.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly IHealthCheckService _service;

        public HealthCheckController(
            IHealthCheckService service,
            IActionResultConverter actionResultConverter
            )
        {
            _service = service;
            _actionResultConverter = actionResultConverter;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return _actionResultConverter.Convert(await _service.CheckDbConnection());
        }
    }
}
