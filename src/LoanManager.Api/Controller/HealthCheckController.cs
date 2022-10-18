using LoanManager.Api.Models;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LoanManager.Api.Controller
{
    public class HealthCheckController : BaseController
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly IHealthCheckService _service;

        public HealthCheckController(
            IHealthCheckService service,
            INotificationHandler notificationHandler,
            IActionResultConverter actionResultConverter
            ):base(notificationHandler)
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
