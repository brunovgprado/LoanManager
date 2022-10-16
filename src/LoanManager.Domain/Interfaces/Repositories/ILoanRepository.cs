using LoanManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface ILoanRepository : IBaseRepository<Guid, Loan>
    {
        Task<bool> FinishLoanAsync(Guid id);
        Task<IEnumerable<Loan>> ReadLoanHistoryByGameAsync(Guid id, int offset, int limit);
        Task<bool> CheckIfGameIsOnLoan(Guid id);
        Task<bool> CheckIfyIfLoanExistsById(Guid id);
    }
}
