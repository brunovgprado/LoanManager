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
        Task EndLoan(Guid id);
        Task<IEnumerable<Loan>> ReadLoanByFriendNameAsync(string name, int offset, int limit);
        Task<IEnumerable<Loan>> ReadLoanHistoryByGameAsync(Guid id, int offset, int limit);
    }
}
