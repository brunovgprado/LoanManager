using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.DomainServices
{
    public interface ILoanDomainService : IGenericService<Guid, Loan>
    {
        void EndLoan(Guid id);
        Task<Loan> ReadLoanByFriendNameAsync(string name);
        Task<IEnumerable<Loan>> ReadLoanHistoryByGameAsync(int offset, int limit);
    }
}
