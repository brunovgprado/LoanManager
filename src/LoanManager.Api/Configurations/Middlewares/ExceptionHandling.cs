using LoanManager.Api.Models.Response;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

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
            catch (SqlException ex)
            {
                await HandleException("An error has occurred with the sql query/command", ex, httpContext, notificationHandler);
            }
            catch (Exception ex)
            {
                await HandleException("An unexpected error occurred", ex, httpContext, notificationHandler);
            }
        }

        private static async Task HandleException(string message, Exception exception, HttpContext httpContext,
            INotificationHandler notificationHandler)
        {
            //TODO: implements application insights Track Exception and refact this handler
            Console.WriteLine($"{message}: " +
                $"Request path: {httpContext.Request.Path} " +
                $"Exception message: {exception.Message} " +
                $"Exception stackTrace: {exception.StackTrace}");

            notificationHandler.AddNotification(new Notification("UnexpectedError", message));
            var defaultReponse = new DefaultResponse(HttpStatusCode.InternalServerError.ToString(), message);

            var status = HttpStatusCode.InternalServerError;
            if (exception.Message.Contains("TimeoutPolicy"))
                status = HttpStatusCode.RequestTimeout;

            await SetReponse(httpContext, status, defaultReponse);
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
