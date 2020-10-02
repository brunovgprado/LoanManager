using LoanManager.Application.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Application.Interfaces.AppServices
{
    public interface IHealthCheckService
    {
        Task<Response<dynamic>> CheckDbConnection();
    }
}
