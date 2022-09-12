namespace LoanManager.Application.Shared
{
    public enum ResponseKind
    {
        InternalServerError = 500,
        BadRequest = 400,
        NotFound = 404,
        Unauthorized = 401,
        Processing = 102,
        Forbidden = 403,
        UnprocessableEntity = 422,
        Created = 201
    }
}
