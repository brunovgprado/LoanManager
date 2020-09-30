using Dapper;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Infrastructure.DataAccess.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly IConfiguration _configuration;

        public FriendRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task CreateAsync(Friend entity)
        {
            var command = @"INSERT INTO Friends (Id, Name, PhoneNumber)
                             VALUES (@Id, @Name, @PhoneNumber)";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var afftectedRows = await connection.ExecuteAsync(command, entity);
            }
        }
        public async Task<IEnumerable<Friend>> ReadAllAsync(int offset, int limit)
        {
            var query = @"SELECT * FROM Friends 
                                ORDER BY Name
                                OFFSET @index ROWS
                                FETCH NEXT @size ROWS ONLY";

            // Aplying pagination limits
            var param = new DynamicParameters();
            param.Add("@index", offset, DbType.Int32);
            param.Add("@size", limit == 0 ? 10 : limit, DbType.Int32);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Friend>(query.ToString(), param);
                return result;
            }
        }

        public async Task<Friend> ReadAsync(Guid id)
        {
            var query = @"SELECT * FROM Friends WHERE Id= @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Friend>(query, param);
                return result.FirstOrDefault();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var command = @"DELETE FROM Friends WHERE Id= @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                await connection.ExecuteAsync(command, param);
            }
        }

        public async void Update(Friend entity)
        {
            var command = @"UPDATE Friends 
                                SET Name = @Name, 
                                    PhoneNumber = @PhoneNumber
                                WHERE Id = @Id";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                await connection.ExecuteAsync(command, entity);
            }
        }
    }
}
