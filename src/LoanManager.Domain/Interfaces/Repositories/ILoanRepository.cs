using LoanManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface ILoanRepository : IGenericRepository<Guid, Loan>
    {
        Task EndLoan(Guid id);
        Task<IEnumerable<Loan>> ReadLoanByFriendNameAsync(string name, int offset, int limit);
        Task<IEnumerable<Loan>> ReadLoanHistoryByGameAsync(Guid id, int offset, int limit);
        Task<bool> CheckIfGameIsOnALoanInProgress(Guid game);
        Task<bool> VerifyIfLoanExsistsById(Guid id);
    }
}
