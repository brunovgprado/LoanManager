using LoanManager.Api.Models;
using LoanManager.Auth.Interfaces.Services;
using LoanManager.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LoanManager.Api.Controller
{
    [Route("api/v1/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly IAuthService _service;

        public AuthController(
            IActionResultConverter actionResultConverter,
            IAuthService service)
        {
            _actionResultConverter = actionResultConverter;
            _service = service;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UserCredentials credentials)
        {
            return _actionResultConverter.Convert(await _service.Authenticate(credentials));
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount([FromBody] UserCredentials credentials)
        {
            return _actionResultConverter.Convert(await _service.CreateAccount(credentials));
        }
    }
}
