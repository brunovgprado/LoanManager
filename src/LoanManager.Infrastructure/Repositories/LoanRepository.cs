using Dapper;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Infrastructure.CrossCutting.Contracts;
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
        public LoanRepository(IEnvConfiguration configuration)
            :base(configuration){}

        public async Task<bool> CreateAsync(Loan entity)
        {
            entity.Id = Guid.NewGuid();
            entity.LoanDate = DateTime.Now;
            
            const string command = @"INSERT INTO Loan (Id, FriendId, GameId, LoanDate, Returned)
                             VALUES (@Id, @FriendId, @GameId, @LoanDate, @Returned)";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(command, entity);

                return affectedRows > 0;
            }
        }
        
        public async Task<IEnumerable<Loan>> GetAsync(int offset, int limit)
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

            using (var connection = new NpgsqlConnection(connectionString))
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

        public async Task<Loan> GetAsync(Guid id)
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

            using (var connection = new NpgsqlConnection(connectionString))
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
        
        public async Task<bool> UpdateAsync(Loan entity)
        {
            const string command = @"UPDATE Loan 
                                SET FriendId = @FriendId, 
                                    GameId = @GameId,
                                    LoanDate = @LoanDate,
                                    LoanStatus = @LoanStatus
                                WHERE Id = @Id";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var affectedLines = await connection.ExecuteAsync(command, entity);
                return affectedLines > 0;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            const string command = @"DELETE FROM Loan WHERE Id = @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var affectedLines = await connection.ExecuteAsync(command, param);
                return affectedLines > 0;
            }
        }

        public async Task<bool> FinishLoanAsync(Guid id)
        {
            const string command = @"UPDATE Loan SET Returned = 't' WHERE Id = @Id";

            var param = new DynamicParameters();
            param.Add("@Id", id, DbType.Guid);

            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var affectedLines = await connection.ExecuteAsync(command, param);
                return affectedLines > 0;
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

            using (var connection = new NpgsqlConnection(connectionString))
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

            using (var connection = new NpgsqlConnection(connectionString))
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
