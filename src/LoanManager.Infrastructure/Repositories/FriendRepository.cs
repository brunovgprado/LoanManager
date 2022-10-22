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
    public class FriendRepository : BaseRepository, IFriendRepository
    {
        public FriendRepository(IEnvConfiguration configuration)
            : base(configuration){}
        
        public async Task<bool> CreateAsync(Friend entity)
        {
            const string command = @"INSERT INTO Friend (Id, Name, PhoneNumber)
                             VALUES (@Id, @Name, @PhoneNumber)";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(command, entity);
                return affectedRows > 0;
            }
        }
        
        public async Task<IEnumerable<Friend>> GetAsync(int offset, int limit)
        {
            const string query = @"SELECT * FROM Friend 
                                ORDER BY Name
                                OFFSET @index ROWS
                                FETCH NEXT @size ROWS ONLY";

            // Applying pagination limits
            var param = new DynamicParameters();
            param.Add("@index", offset, DbType.Int32);
            param.Add("@size", limit == 0 ? 10 : limit, DbType.Int32);

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Friend>(query.ToString(), param);
                return result;
            }
        }

        public async Task<Friend> GetAsync(Guid id)
        {
            const string query = @"SELECT * FROM Friend WHERE Id= @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Friend>(query, param);
                return result.FirstOrDefault();
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            const string command = @"DELETE FROM Friend WHERE Id= @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var affectedLines = await connection.ExecuteAsync(command, param);
                return affectedLines > 0;
            }
        }

        public async Task<bool> UpdateAsync(Friend entity)
        {
            const string command = @"UPDATE Friend 
                                SET Name = @Name, 
                                    PhoneNumber = @PhoneNumber
                                WHERE Id = @Id";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var affectedLines = await connection.ExecuteAsync(command, entity);
                return affectedLines > 0;
            }
        }

        public async Task<bool> CheckIfFriendExistsById(Guid id)
        {
            return await CheckIfEntityExistsById(id, "Friend");
        }
    }
}
