using Dapper;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManager.Infrastructure.DataAccess.Repositories
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public GameRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        }
        
        public async Task<int> CreateAsync(Game entity)
        {
            const string command = @"INSERT INTO Game (Id, Title, Description, Genre, Platform)
                             VALUES (@Id, @Title, @Description, @Genre, @Platform)";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(command, entity);               
            }
        }
        
        public async Task<IEnumerable<Game>> ReadAllAsync(int offset, int limit)
        {
            const string query = @"SELECT Ga.Id, 
                                 Ga.Title, 
                                 Ga.Description, 
                                 Ga.Platform,
                                 Ga.Genre,
                                    (SELECT CASE WHEN
                                    EXISTS(SELECT Lo.Returned FROM Loan Lo 
                                            WHERE Lo.GameId = Ga.Id 
                                                and Lo.Returned <> 't')
                                    THEN CAST(1 AS BIT) 
                		            ELSE CAST(0 AS BIT) END)
                                    OnLoan
                                FROM Game Ga
                                ORDER BY Title
                                OFFSET @index ROWS
                                FETCH NEXT @size ROWS ONLY";

            // Applying pagination limits
            var param = new DynamicParameters();
            param.Add("@index", offset, DbType.Int32);
            param.Add("@size", limit == 0 ? 10 : limit, DbType.Int32);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Game>(query.ToString(), param);
                return result;
            }
        }

        public async Task<Game> ReadAsync(Guid id)
        {
            const string query = @"SELECT Ga.Id, 
                                 Ga.Title, 
                                 Ga.Description, 
                                 Ga.Platform,
                                 Ga.Genre,
                                    (SELECT CASE WHEN
                                    EXISTS(SELECT 1 FROM Loan Lo 
                                            WHERE Lo.GameId = Ga.Id 
                                                and Lo.Returned <> 't')
                                    THEN CAST(1 AS BIT) 
                		            ELSE CAST(0 AS BIT) END)
                                    OnLoan
                                FROM Game Ga
                                WHERE Id= @Id
                                ORDER BY Ga.Title";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Game>(query, param);
                return result.FirstOrDefault();
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            const string command = @"DELETE FROM Game WHERE Id= @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(command, param);
            }
        }

        public async Task<int> Update(Game entity)
        {
            const string command = @"UPDATE Game 
                                SET Title = @Title, 
                                    Description = @Description, 
                                    Genre = @Genre, 
                                    Platform = @Platform
                                WHERE Id = @Id";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(command, entity);
            }
        }

        public async Task<bool> CheckIfGameExistsById(Guid id)
        {
            return await CheckIfEntityExistsById(id, "Games");
        }
    }
}
