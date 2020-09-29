using Dapper;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Infrastructure.DataAccess.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly IConfiguration _configuration;

        public GameRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task CreateAsync(Game entity)
        {            
            var command = @"INSERT INTO Games (Id, Title, Description, Genre, Platform)
                             VALUES (@Id, @Title, @Description, @Genre, @Platform)";
            
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var afftectedRows = await connection.ExecuteAsync(command, entity);               
            }
        }
        public async Task<IEnumerable<Game>> ReadAllAsync(int offset, int limit)
        {
            var query = "SELECT * FROM Games WHERE Id= @Id";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Game>(query.ToString());
                return result;
            }
        }

        public async Task<Game> ReadAsync(Guid id)
        {
            var query = @"SELECT * FROM Games WHERE Id= @Id";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Game>(query, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var command = @"DELETE FROM Games WHERE Id= @Id";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                await connection.ExecuteAsync(command, new {Id = id});
            }
        }
        public async void Update(Game entity)
        {
            var command = @"UPDATE Games 
                                SET Title = @Title, 
                                    Description = @Description, 
                                    Genre = @Genre, 
                                    Platform = @Platform
                                WHERE Id = @Id";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                await connection.ExecuteAsync(command, entity);
            }
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

    }
}
