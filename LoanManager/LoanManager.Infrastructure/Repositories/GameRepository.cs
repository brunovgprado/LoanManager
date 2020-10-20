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
    public class GameRepository : IGameRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public GameRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        }

        #region CRUD Operations
        public async Task CreateAsync(Game entity)
        {            
            var command = @"INSERT INTO Games (Id, Title, Description, Genre, Platform)
                             VALUES (@Id, @Title, @Description, @Genre, @Platform)";
            
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var afftectedRows = await connection.ExecuteAsync(command, entity);               
            }
        }
        public async Task<IEnumerable<Game>> ReadAllAsync(int offset, int limit)
        {
            var query = @"SELECT Ga.Id, 
                                 Ga.Title, 
                                 Ga.Description, 
                                 Ga.Platform,
                                 Ga.Genre,
                                    (SELECT CASE WHEN
                                    EXISTS(SELECT Lo.Returned FROM Loans Lo 
                                            WHERE Lo.GameId = Ga.Id 
                                                and Lo.Returned <> 't')
                                    THEN CAST(1 AS BIT) 
                		            ELSE CAST(0 AS BIT) END)
                                    OnLoan
                                FROM Games Ga
                                ORDER BY Title
                                OFFSET @index ROWS
                                FETCH NEXT @size ROWS ONLY";

            // Aplying pagination limits
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
            var query = @"SELECT Ga.Id, 
                                 Ga.Title, 
                                 Ga.Description, 
                                 Ga.Platform,
                                 Ga.Genre,
                                    (SELECT CASE WHEN
                                    EXISTS(SELECT 1 FROM Loans Lo 
                                            WHERE Lo.GameId = Ga.Id 
                                                and Lo.Returned <> 't')
                                    THEN CAST(1 AS BIT) 
                		            ELSE CAST(0 AS BIT) END)
                                    OnLoan
                                FROM Games Ga
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

        public async Task DeleteAsync(Guid id)
        {
            var command = @"DELETE FROM Games WHERE Id= @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                await connection.ExecuteAsync(command, param);
            }
        }

        public async Task Update(Game entity)
        {
            var command = @"UPDATE Games 
                                SET Title = @Title, 
                                    Description = @Description, 
                                    Genre = @Genre, 
                                    Platform = @Platform
                                WHERE Id = @Id";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                await connection.ExecuteAsync(command, entity);
            }
        }
        #endregion

        public async Task<bool> VerifyIfGameExsistsById(Guid id)
        {
            var query = @"SELECT CASE WHEN
                                EXISTS(SELECT 1 FROM Games
                                        WHERE Id = @Id)
                                THEN CAST(1 AS BIT) 
                		        ELSE CAST(0 AS BIT) END";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<bool>(query, param);
                return result.FirstOrDefault();
            }
        }
    }
}
