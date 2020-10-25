using LoanManager.Application.Shared;
using LoanManager.Auth.Models;
using System.Threading.Tasks;

namespace LoanManager.Auth.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Response<UserResponse>> Authenticate(UserCredentials credentials);
        Task<Response<UserResponse>> CreateAccount(UserCredentials credentials);
    }
}
