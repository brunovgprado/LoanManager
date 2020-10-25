using Dapper;
using LoanManager.Auth.Interfaces.Repository;
using LoanManager.Auth.Models;
using Npgsql;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManager.Infrastructure.DataAccess.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _connectionString;

        public AuthRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        }

        public async Task<User> GetUser(User credentials)
        {
            var query = @"SELECT * FROM Users WHERE Email = @Email";

            var param = new DynamicParameters();
            param.Add("@Email", credentials.Email, DbType.String);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<User>(query, param);
                return result.FirstOrDefault();
            }
        }

        public async Task CreateAccount(User credentials)
        {
            var command = @"INSERT INTO Users (Id, Email, Password)
                             VALUES (@Id, @Email, @Password)";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var afftectedRows = await connection.ExecuteAsync(command, credentials);
            }
        }

        public async Task<bool> CheckIfUserAlreadyExistis(User credentials)
        {
            var query = @"SELECT CASE WHEN EXISTS 
                (SELECT 1 FROM Users WHERE Email = @Email)
                THEN CAST (1 AS BIT) 
                ELSE CAST (0 AS BIT) END";

            var param = new DynamicParameters();
            param.Add("@Email", credentials.Email, DbType.String);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<bool>(query, param);
                return result.FirstOrDefault();
            }
        }
    }
}
