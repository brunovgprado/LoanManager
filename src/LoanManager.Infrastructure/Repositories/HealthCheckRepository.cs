using Dapper;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Infrastructure.CrossCutting.Contracts;
using Npgsql;
using System.Threading.Tasks;

namespace LoanManager.Infrastructure.DataAccess.Repositories
{
    public class HealthCheckRepository : BaseRepository, IHealthCheckRepository
    {
        public HealthCheckRepository(IEnvConfiguration configuration)
            : base(configuration){}

        public async Task<bool> CheckDbConnection()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<bool>("SELECT 1");
            }
        }
    }
}
