﻿using LoanManager.Api.Models;
using LoanManager.Auth.Interfaces.Services;
using LoanManager.Auth.Models;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace LoanManager.Api.Controller
{
    public class AuthController : BaseController
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly IAuthService _service;

        public AuthController(
            IActionResultConverter actionResultConverter,
            INotificationHandler notificationHandler,
            IAuthService service)
            :base(notificationHandler)
        {
            _actionResultConverter = actionResultConverter;
            _service = service;
        }

        [HttpPost("signin")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Authenticate([FromBody] UserCredentials credentials)
        {
            return _actionResultConverter.Convert(await _service.Authenticate(credentials));
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateAccount([FromBody] UserCredentials credentials)
        {
            return _actionResultConverter.Convert(await _service.CreateAccount(credentials));
        }
    }
}
