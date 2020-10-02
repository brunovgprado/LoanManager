using Dapper;
using LoanManager.Domain.Interfaces.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Infrastructure.DataAccess.Repositories
{
    public class HealthCheckRepository : IHealthCheckRepository
    {
        private readonly string _connectionString;

        public HealthCheckRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        }

        public async Task<bool> CheckDbConnection()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<bool>("SELECT 1");
            }
        }
    }
}
