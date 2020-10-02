using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Properties;
using LoanManager.Application.Shared;
using LoanManager.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
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
            try
            {
                // Checking database connection
                var databaseConnectionIsOK = await _healthCheckRepository.CheckDbConnection();


                if(databaseConnectionIsOK)
                    return response.SetResult(
                        new { 
                            App = "API is working done!", 
                            DatabaseConnection = "DB connection is working done!" 
                        });

                return response.SetResult(
                    new { 
                        App = "API is working done!", 
                        DatabaseConnection = "Has some problem with DB connection" 
                    });
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileCheckingDatabaseConnection);
            }
        }
    }
}
