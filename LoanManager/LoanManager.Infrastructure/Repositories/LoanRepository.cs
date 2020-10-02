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
using Npgsql;

namespace LoanManager.Infrastructure.DataAccess.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly string _connectionString;

        public LoanRepository(IConfiguration configuration)
        {
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        }

        public async Task CreateAsync(Loan entity)
        {
            var command = @"INSERT INTO Loans (Id, FriendId, GameId, LoanDate, Returned)
                             VALUES (@Id, @FriendId, @GameId, @LoanDate, @Returned)";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var afftectedRows = await connection.ExecuteAsync(command, entity);
            }
        }
        public async Task<IEnumerable<Loan>> ReadAllAsync(int offset, int limit)
        {
            var query = @"SELECT * FROM Loans AS Lo
                            JOIN Friends AS Fr ON Fr.Id = Lo.FriendId
                            JOIN Games AS Ga ON Ga.Id = Lo.GameId 
                            ORDER BY LoanDate
                            OFFSET @index ROWS
                            FETCH NEXT @size ROWS ONLY";

            // Aplying pagination limits
            var param = new DynamicParameters();
            param.Add("@index", offset, DbType.Int32);
            param.Add("@size", limit == 0 ? 10 : limit, DbType.Int32);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Loan, Friend, Game, Loan>(query,
                    map: (loan, friend, game) =>
                    {
                        loan.Friend = friend;
                        loan.Game = game;
                        return loan;
                    }
                    , param);
                return result;
            }
        }

        public async Task<Loan> ReadAsync(Guid id)
        {
            var query = @"SELECT * FROM Loans WHERE Id= @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(_connectionString))
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

            using (var connection = new NpgsqlConnection(_connectionString))
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

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                await connection.ExecuteAsync(command, param);
            }
        }

        public async Task EndLoan(Guid id)
        {
            var command = @"UPDATE Loans SET Returned = 't' WHERE Id = @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                await connection.ExecuteAsync(command, param);
            }
        }

        public async Task<IEnumerable<Loan>> ReadLoanByFriendNameAsync(string name, int offset, int limit)
        {
            var query = @"SELECT * FROM Loans AS Lo
                            JOIN Friends AS Fr ON Fr.Id = Lo.FriendId
                            JOIN Games AS Ga ON Ga.Id = Lo.GameId
                            WHERE Fr.Name LIKE CONCAT('%', @name, '%') 
                            ORDER BY Name
                            OFFSET @index ROWS
                            FETCH NEXT @size ROWS ONLY";

            // Aplying pagination limits and parameters
            var param = new DynamicParameters();
            param.Add("@index", offset, DbType.Int32);
            param.Add("@size", limit == 0 ? 10 : limit, DbType.Int32);
            param.Add("@name", name);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Loan, Friend, Game, Loan>(query,
                    map: (loan, friend, game) => 
                    {
                        loan.Friend = friend;
                        loan.Game = game;
                        return loan;
                    }
                    , param);
                return result;
            }
        }

        public async Task<IEnumerable<Loan>> ReadLoanHistoryByGameAsync(Guid id, int offset, int limit)
        {
            var query = @"SELECT * FROM Loans AS Lo
                            JOIN Friends AS Fr ON Fr.Id = Lo.FriendId
                            JOIN Games AS Ga ON Ga.Id = Lo.GameId
                            WHERE Ga.Id = @Id 
                            ORDER BY Name
                            OFFSET @index ROWS
                            FETCH NEXT @size ROWS ONLY";

            // Aplying pagination limits and parameters
            var param = new DynamicParameters();
            param.Add("@index", offset, DbType.Int32);
            param.Add("@size", limit == 0 ? 10 : limit, DbType.Int32);
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Loan, Friend, Game, Loan>(query,
                    map: (loan, friend, game) =>
                    {
                        loan.Friend = friend;
                        loan.Game = game;
                        return loan;
                    }
                    , param);
                return result;
            }

        }

        public async Task<bool> CheckIfGameIsOnALoanInProgress(Guid id)
        {
            var query = @"SELECT CASE WHEN EXISTS 
                (SELECT 1 FROM Loans WHERE GameId = @GameId)
                THEN CAST (1 AS BIT) 
                ELSE CAST (0 AS BIT) END";

            var param = new DynamicParameters();
            param.Add("@GameId", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<bool>(query, param);
                return result.FirstOrDefault();
            }
        }
    }
}
