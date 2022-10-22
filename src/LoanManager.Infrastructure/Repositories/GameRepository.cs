using Dapper;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Infrastructure.CrossCutting.Contracts;
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
        public GameRepository(IEnvConfiguration configuration)
            :base(configuration)
        {
        }
        
        public async Task<bool> CreateAsync(Game entity)
        {
            entity.Id = Guid.NewGuid();
            
            const string command = @"INSERT INTO Game (Id, Title, Description, Genre, Platform)
                             VALUES (@Id, @Title, @Description, @Genre, @Platform)";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(command, entity);
                return affectedRows > 0;
            }
        }
        
        public async Task<IEnumerable<Game>> GetAsync(int offset, int limit)
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

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Game>(query.ToString(), param);
                return result;
            }
        }

        public async Task<Game> GetAsync(Guid id)
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

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Game>(query, param);
                return result.FirstOrDefault();
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            const string command = @"DELETE FROM Game WHERE Id= @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var affectedLines = await connection.ExecuteAsync(command, param);
                return affectedLines > 0;
            }
        }

        public async Task<bool> UpdateAsync(Game entity)
        {
            const string command = @"UPDATE Game 
                                SET Title = @Title, 
                                    Description = @Description, 
                                    Genre = @Genre, 
                                    Platform = @Platform
                                WHERE Id = @Id";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var affectedLines = await connection.ExecuteAsync(command, entity);
                return affectedLines > 0;
            }
        }

        public async Task<bool> CheckIfGameExistsById(Guid id)
        {
            return await CheckIfEntityExistsById(id, "Games");
        }
    }
}
