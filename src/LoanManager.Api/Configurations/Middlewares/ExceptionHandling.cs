using LoanManager.Api.Models.Response;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace LoanManager.Api.Configurations.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext,
            INotificationHandler notificationHandler)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //TODO: implements application insights Track Exception and refact this handler
                Console.WriteLine($"An unexpected error occurred: " +
                    $"Request path: {httpContext.Request.Path} " +
                    $"Exception message: {ex.Message} " +
                    $"Exception stackTrace: {ex.StackTrace}");

                notificationHandler.AddNotification(new Notification("UnexpectedError", "An unexpected error occurred"));
                var defaultReponse = new DefaultResponse(HttpStatusCode.InternalServerError.ToString(), "An unexpected error occurred");

                var status = HttpStatusCode.InternalServerError;
                if (ex.Message.Contains("TimeoutPolicy"))
                    status = HttpStatusCode.RequestTimeout;

                await SetReponse(httpContext, status, defaultReponse);
            }
        }

        private static async Task<string> SetReponse(HttpContext httpContext, HttpStatusCode statusCode, DefaultResponse response)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            string result = JsonSerializer.Serialize(response, options);
            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = MediaTypeNames.Application.Json;
            await httpContext.Response.WriteAsync(result);
            return result;
        }
    }
}
