using FluentValidation;
using LoanManager.Application.Models.Shared;
using System.Collections.Generic;
using System.Linq;

namespace LoanManager.Application.Shared
{
    public class Response<T> : IResponse
    {
        private UseCaseResponseKind ErrorKind { get; set; }
        public string ErrorMessage { get; private set; }
        public IEnumerable<ErrorMessage> Errors { get; private set; }

        public T Result { get; private set; }

        public Response<T> SetResult(T result)
        {
            ErrorMessage = null;
            Result = result;

            return this;
        }

        public Response<T> SetError(string errorMessage, UseCaseResponseKind errorKind, IEnumerable<ErrorMessage> errors = null)
        {
            ErrorMessage = errorMessage;
            ErrorKind = errorKind;
            Errors = errors;

            return this;
        }

        public Response<T> SetInternalServerError(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetError(errorMessage, UseCaseResponseKind.InternalServerError, errors);
        }

        public Response<T> SetBadRequest(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetError(errorMessage, UseCaseResponseKind.BadRequest, errors);
        }

        public Response<T> SetNotFound(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetError(errorMessage, UseCaseResponseKind.NotFound, errors);
        }

        public bool Success()
        {
            return ReferenceEquals(ErrorMessage, null);
        }

        public UseCaseResponseKind? GetErrorKind()
        {
            if (ReferenceEquals(ErrorMessage, null))
                return null;

            return ErrorKind;
        }

        public Response<T> SetRequestValidationError(ValidationException ex)
        {
            return SetRequestValidationError("Validation exception", ex.Errors.Select(error => new ErrorMessage(error.ErrorCode, error.ErrorMessage)));
        }

        public Response<T> SetRequestValidationError(string errorMessage, IEnumerable<ErrorMessage> errors = null)
        {
            return SetStatus(UseCaseResponseKind.UnprocessableEntity, errorMessage, errors);
        }

        public Response<T> SetStatus(UseCaseResponseKind status, string errorMessage = null, IEnumerable<ErrorMessage> errors = null)
        {
            ErrorKind = status;
            ErrorMessage = errorMessage;
            Errors = errors;

            return this;
        }
    }
}
