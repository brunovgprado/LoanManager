using LoanManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.DomainServices
{
    public interface ILoanDomainService : IGenericService<Guid, Loan>
    {
        Task<bool> FinishLoanAsync(Guid id);
        Task<IEnumerable<Loan>> ReadLoanHistoryByGameAsync(Guid id, int offset, int limit);
    }
}
