using System.Collections.Generic;

#nullable enable
namespace LoanManager.Api.Models.Response
{
    public class DefaultResponse
    {
        private readonly bool _success;
        private readonly string? _code;
        private readonly string? _message;
        private readonly object? _data;
        private readonly IEnumerable<KeyValuePair<string, string>> _errors;

        public bool Success { get => _success; }
        public string? Code { get => _code; }
        public string? Message { get => _message; }
        public object? Data { get => _data; }
        public IEnumerable<KeyValuePair<string, string>> Errors { get => _errors; }


        public DefaultResponse(string? code = null,
            string? message = null)
        {
            _code = code;
            _message = message;
        }

        public DefaultResponse(object? data,
            string? code = null,
            string? message = null)
        {
            _success = true;
            _code = code;
            _message = message;
            _data = data;
        }

        public DefaultResponse(IEnumerable<KeyValuePair<string, string>> errors,
            string? code = null,
            string? message = null)
        {
            _success = false;
            _code = code;
            _message = message;
            _errors = errors;
        }
    }
}