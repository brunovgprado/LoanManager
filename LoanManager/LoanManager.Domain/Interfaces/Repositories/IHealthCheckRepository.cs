using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface IHealthCheckRepository
    {
        Task<bool> CheckDbConnection();
    }
}
