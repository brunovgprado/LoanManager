﻿using LoanManager.Api.Models.Response;
using LoanManager.Api.Properties;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LoanManager.Api.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly INotificationHandler _notificationHandler;

        public BaseController(INotificationHandler notificationHandler)
        {
            _notificationHandler = notificationHandler;
        }

        protected IActionResult CreateResult(object data = null, HttpStatusCode? statusCode = HttpStatusCode.OK)
        {
            if (_notificationHandler.GetInstance().HasNotifications)
            {
                IEnumerable<Notification> notifications = _notificationHandler.GetInstance().Notifications;

                var unexpectedError = notifications.Any(x => x.Key.Equals("UnexpectedError"));
                var inputValidation = notifications.Any(x => x.Key.Equals("InputValidation"));
                var invalidCredentials = notifications.Any(x => x.Key.Equals("InvalidCredentials"));
                var notFound = notifications.Any(x => x.Key.Equals("NotFound"));
                var businessRule = notifications.Any(x => x.Key.Equals("BusinessRule"));

                if (unexpectedError)
                    return CreateUnexpectedErrorResult();

                if (inputValidation) 
                    return CreateInputValidationErrorResult();

                if (invalidCredentials)
                    return CreateInvalidCredentialsErrorResult();

                if (notFound)
                    return CreateNotFoundErrorResult();

                if (businessRule)
                    return CreateBusinessRuleErroResult();
            }

            return Ok(new DefaultResponse(data: data));
        }

        private IActionResult CreateUnexpectedErrorResult()
        {
            return StatusCode(500);
        }

        private IActionResult CreateBusinessRuleErroResult()
        {
            var errors = ConvertNotificationsToDictionary(_notificationHandler.GetInstance().Notifications);

            return Conflict(new DefaultResponse(errors: errors, code: "BusinessRule", 
                message: Resources.BusinessRuleBroken));
        }

        private IActionResult CreateNotFoundErrorResult()
        {
            var errors = ConvertNotificationsToDictionary(_notificationHandler.GetInstance().Notifications);

            return NotFound(new DefaultResponse(errors: errors, code: "NotFound",
                message: Resources.NotFound));
        }

        private IActionResult CreateInputValidationErrorResult()
        {
            var errors = ConvertNotificationsToDictionary(_notificationHandler.GetInstance().Notifications);

            return BadRequest(new DefaultResponse(errors: errors, code: "InputValidation",
                message: Resources.InputValidationError));
        }

        private IActionResult CreateInvalidCredentialsErrorResult()
        {
            var errors = ConvertNotificationsToDictionary(_notificationHandler.GetInstance().Notifications);

            return Unauthorized(new DefaultResponse(errors: errors, code: "InvalidCredentials",
                message: Resources.InvalidCredentials));
        }

        private IEnumerable<KeyValuePair<string, string>> ConvertNotificationsToDictionary(IEnumerable<Notification> notifications)
        {
            var result = notifications.DistinctBy(n => n.Message)
                .Select(n => new KeyValuePair<string, string>(n.Key, n.Message));
            return result;
        }
    }
}