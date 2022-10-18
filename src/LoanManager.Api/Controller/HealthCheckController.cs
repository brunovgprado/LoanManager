using LoanManager.Api.Models;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LoanManager.Api.Controller
{
    public class HealthCheckController : BaseController
    {
        private readonly IHealthCheckService _service;

        public HealthCheckController(
            IHealthCheckService service,
            INotificationHandler notificationHandler
        ):base(notificationHandler)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateResult(await _service.CheckDbConnection());
        }
    }
}
