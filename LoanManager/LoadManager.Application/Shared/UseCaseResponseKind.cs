
namespace LoanManager.Application.Shared
{
    public enum UseCaseResponseKind
    {
        InternalServerError = 500,
        BadRequest = 400,
        NotFound = 404,
        Unauthorized = 401,
        Processing = 102,
        Forbidden = 403,
        UnprocessableEntity = 422
    }
}
