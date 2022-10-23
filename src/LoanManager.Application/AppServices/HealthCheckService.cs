using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Properties;
using LoanManager.Application.Shared;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LoanManager.Application.AppServices
{
    public class HealthCheckService : IHealthCheckService
    {
        public IHealthCheckRepository _healthCheckRepository;

        public HealthCheckService(IHealthCheckRepository healthCheckRepository)
        {
            _healthCheckRepository = healthCheckRepository;
        }

        public async Task<Response<dynamic>> CheckDbConnection()
        {
            var response = new Response<dynamic>();

            // Checking database connection
            var databaseConnectionIsOK = await _healthCheckRepository.CheckDbConnection();


            if (databaseConnectionIsOK)
                return response.SetResult(
                    new
                    {
                        DatabaseConnection = "DB connection is working done!"
                    });
            return response.SetResult(
                new
                {
                    DatabaseConnection = "There are some problem with DB connection"
                });
        }
    }
}
