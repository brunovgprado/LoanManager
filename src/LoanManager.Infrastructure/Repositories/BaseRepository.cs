using Npgsql;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LoanManager.Infrastructure.CrossCutting.Contracts;

namespace LoanManager.Infrastructure.DataAccess.Repositories
{
    public class BaseRepository
    {
        protected readonly string connectionString;
        protected readonly IEnvConfiguration configuration;

        protected BaseRepository(IEnvConfiguration configuration)
        {
            connectionString = configuration.KV_DB_CONNECTIONSTRING;
        }
        
        protected async Task<bool> CheckIfEntityExistsById(Guid id, string entity)
        {
            var query = $@"SELECT CASE WHEN
                                EXISTS(SELECT 1 FROM {entity}
                                        WHERE Id = @Id)
                                THEN CAST(1 AS BIT) 
                		        ELSE CAST(0 AS BIT) END";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<bool>(query, param);
                return result.FirstOrDefault();
            }
        }
    }
}