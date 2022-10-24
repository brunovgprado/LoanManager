using LoanManager.Application.Shared;
using LoanManager.Auth.Models;
using System.Threading.Tasks;

namespace LoanManager.Auth.Interfaces.Services
{
    public interface IAuthService
    {
        Task<UserResponse> Authenticate(UserCredentials credentials);
        Task<UserResponse> CreateAccount(UserCredentials credentials);
    }
}
