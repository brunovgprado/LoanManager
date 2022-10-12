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
    public class LoanRepository : BaseRepository, ILoanRepository
    {
        private readonly string _connectionString;

        public LoanRepository(IConfiguration configuration)
        {
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        }

        public async Task<int> CreateAsync(Loan entity)
        {
            const string command = @"INSERT INTO Loan (Id, FriendId, GameId, LoanDate, Returned)
                             VALUES (@Id, @FriendId, @GameId, @LoanDate, @Returned)";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(command, entity);
            }
        }
        
        public async Task<IEnumerable<Loan>> ReadAllAsync(int offset, int limit)
        {
            const string query = @"SELECT *, (SELECT CASE WHEN
                                    (Lo.Returned <> 't')
                                    THEN CAST(1 AS BIT) 
                		            ELSE CAST(0 AS BIT) END)
                                    OnLoan
                            FROM Loan AS Lo
                            JOIN Friend AS Fr ON Fr.Id = Lo.FriendId
                            JOIN Game AS Ga ON Ga.Id = Lo.GameId 
                            ORDER BY LoanDate
                            OFFSET @index ROWS
                            FETCH NEXT @size ROWS ONLY";

            // Applying pagination limits
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
            const string query = @"SELECT *, (SELECT CASE WHEN
                                    (Lo.Returned <> 't')
                                    THEN CAST(1 AS BIT) 
                		            ELSE CAST(0 AS BIT) END)
                                    OnLoan
                            FROM Loan AS Lo
                            JOIN Friend AS Fr ON Fr.Id = Lo.FriendId
                            JOIN Game AS Ga ON Ga.Id = Lo.GameId 
                            WHERE Lo.Id = @Id";

            var param = new DynamicParameters();
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
                return result.FirstOrDefault();
            }
        }
        
        public async Task<int> Update(Loan entity)
        {
            const string command = @"UPDATE Loan 
                                SET FriendId = @FriendId, 
                                    GameId = @GameId,
                                    LoanDate = @LoanDate,
                                    LoanStatus = @LoanStatus
                                WHERE Id = @Id";

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(command, entity);
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            const string command = @"DELETE FROM Loan WHERE Id = @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(command, param);
            }
        }

        public async Task<int> EndLoan(Guid id)
        {
            const string command = @"UPDATE Loan SET Returned = 't' WHERE Id = @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(command, param);
            }
        }

        public async Task<IEnumerable<Loan>> ReadLoanByFriendNameAsync(string name, int offset, int limit)
        {
            const string query = @"SELECT *, (SELECT CASE WHEN
                                    (Lo.Returned <> 't')
                                    THEN CAST(1 AS BIT) 
                		            ELSE CAST(0 AS BIT) END)
                                    OnLoan
                            FROM Loan AS Lo
                            JOIN Friend AS Fr ON Fr.Id = Lo.FriendId
                            JOIN Game AS Ga ON Ga.Id = Lo.GameId
                            WHERE Fr.Name LIKE CONCAT('%', @name, '%') 
                            ORDER BY Name
                            OFFSET @index ROWS
                            FETCH NEXT @size ROWS ONLY";

            // Applying pagination limits and parameters
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
            const string query = @"SELECT *, (SELECT CASE WHEN
                                    (Lo.Returned <> 't')
                                    THEN CAST(1 AS BIT) 
                		            ELSE CAST(0 AS BIT) END)
                                    OnLoan 
                            FROM Loan AS Lo
                            JOIN Friend AS Fr ON Fr.Id = Lo.FriendId
                            JOIN Game AS Ga ON Ga.Id = Lo.GameId
                            WHERE Ga.Id = @Id 
                            ORDER BY Name
                            OFFSET @index ROWS
                            FETCH NEXT @size ROWS ONLY";

            // Applying pagination limits and parameters
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

        public async Task<bool> CheckIfGameIsOnLoan(Guid id)
        {
            const string query = @"SELECT CASE WHEN EXISTS 
                (SELECT 1 FROM Loan WHERE GameId = @GameId and Returned = 'f')
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

        public async Task<bool> CheckIfyIfLoanExistsById(Guid id)
        {
            return await CheckIfEntityExistsById(id, "Loans");
        }
    }
}
