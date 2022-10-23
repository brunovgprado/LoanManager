using Dapper;
using LoanManager.Auth.Interfaces.Repository;
using LoanManager.Auth.Models;
using LoanManager.Infrastructure.CrossCutting.Contracts;
using Npgsql;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManager.Infrastructure.DataAccess.Repositories
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {

        public AuthRepository(IEnvConfiguration configuration)
         :base(configuration){}

        public async Task<User> GetUser(User credentials)
        {
            const string query = @"SELECT * FROM UserAccount WHERE Email = @Email";

            var param = new DynamicParameters();
            param.Add("@Email", credentials.Email, DbType.String);

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<User>(query, param);
                return result.FirstOrDefault();
            }
        }

        public async Task CreateAccount(User credentials)
        {
            const string command = @"INSERT INTO UserAccount (Id, Email, Password)
                             VALUES (@Id, @Email, @Password)";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var afftectedRows = await connection.ExecuteAsync(command, credentials);
            }
        }

        public async Task<bool> CheckIfUserAlreadyExistis(User credentials)
        {
            const string query = @"SELECT CASE WHEN EXISTS 
                (SELECT 1 FROM UserAccount WHERE Email = @Email)
                THEN CAST (1 AS BIT) 
                ELSE CAST (0 AS BIT) END";

            var param = new DynamicParameters();
            param.Add("@Email", credentials.Email, DbType.String);

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<bool>(query, param);
                return result.FirstOrDefault();
            }
        }
    }
}
