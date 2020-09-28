using LoanManager.Domain.Entities;
using System;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface ILoanRepository : IGenericRepository<Guid, Loan>
    {
    }
}
