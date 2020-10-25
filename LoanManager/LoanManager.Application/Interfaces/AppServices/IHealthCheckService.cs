using LoanManager.Application.Shared;
using System.Threading.Tasks;

namespace LoanManager.Application.Interfaces.AppServices
{
    public interface IHealthCheckService
    {
        Task<Response<dynamic>> CheckDbConnection();
    }
}
