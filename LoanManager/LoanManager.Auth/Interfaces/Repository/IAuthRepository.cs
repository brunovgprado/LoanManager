using LoanManager.Auth.Models;
using System.Threading.Tasks;

namespace LoanManager.Auth.Interfaces.Repository
{
    public interface IAuthRepository
    {
        Task<User> GetUser(User credentials);
        Task CreateAccount(User credentials);
        Task<bool> CheckIfUserAlreadyExistis(User credentials);
    }
}
