using LoanManager.Application.Models.Shared;
using LoanManager.Application.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoanManager.Api.Models
{
    public interface IActionResultConverter
    {
        IActionResult Convert<T>(Response<T> response);
    }

    public class ActionResultConverter : IActionResultConverter
    {
        private readonly string _path;

        public ActionResultConverter(IHttpContextAccessor accessor)
        {
            _path = accessor.HttpContext.Request.Path.Value;
        }

        public IActionResult Convert<T>(Response<T> response)
        {
            if (response == null)
                return BuildError(new[] { new ErrorMessage("000", "ActionResultConverter Error") }, ResponseKind.InternalServerError);

            if (response.Success())
            {
                return new OkObjectResult(response.Result);
            }
            else
            {
                var useCaseResponseErrorKind = response.GetErrorKind();
                if (useCaseResponseErrorKind == null)
                    return BuildError(new[] { new ErrorMessage("000", "ActionResultConverter Error") }, ResponseKind.InternalServerError);

                var hasErrors = response.Errors == null || !response.Errors.Any();
                var errorResult = hasErrors
                    ? new[] { new ErrorMessage("000", response.ErrorMessage ?? "Unknown error") }
                    : response.Errors;

                return BuildError(errorResult, useCaseResponseErrorKind.Value);
            }
        }

        private ObjectResult BuildError(object data, ResponseKind statusCode)
        {
            if (statusCode == ResponseKind.InternalServerError)
                Console.Write($"[ERROR] {_path} ({{@data}})", data);

            return new ObjectResult(data)
            {
                StatusCode = (int)statusCode
            };
        }
    }
}
