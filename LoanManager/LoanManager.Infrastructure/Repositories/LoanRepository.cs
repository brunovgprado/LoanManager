using Microsoft.Extensions.Configuration;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Linq;

namespace LoanManager.Infrastructure.DataAccess.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly IConfiguration _configuration;

        public LoanRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task CreateAsync(Loan entity)
        {
            var command = @"INSERT INTO Loans (Id, FriendId, GameId, LoanDate, LoanStatus)
                             VALUES (@Id, @FriendId, @GameId, @LoanDate, @LoanStatus)";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var afftectedRows = await connection.ExecuteAsync(command, entity);
            }
        }
        public async Task<IEnumerable<Loan>> ReadAllAsync(int offset, int limit)
        {
            var query = @"SELECT * FROM Loans 
                                ORDER BY LoanDate
                                OFFSET @index ROWS
                                FETCH NEXT @size ROWS ONLY";

            // Aplying pagination limits
            var param = new DynamicParameters();
            param.Add("@index", offset, DbType.Int32);
            param.Add("@size", limit == 0 ? 10 : limit, DbType.Int32);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Loan>(query.ToString(), param);
                return result;
            }
        }

        public async Task<Loan> ReadAsync(Guid id)
        {
            var query = @"SELECT * FROM Loans WHERE Id= @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Loan>(query, param);
                return result.FirstOrDefault();
            }
        }
        public async void Update(Loan entity)
        {
            var command = @"UPDATE Loans 
                                SET FriendId = @FriendId, 
                                    GameId = @GameId,
                                    LoanDate = @LoanDate,
                                    LoanStatus = @LoanStatus
                                WHERE Id = @Id";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                await connection.ExecuteAsync(command, entity);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var command = @"DELETE FROM Loans WHERE Id = @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                await connection.ExecuteAsync(command, param);
            }
        }
    }
}
