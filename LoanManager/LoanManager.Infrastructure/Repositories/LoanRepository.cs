using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Infrastructure.DataAccess.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        public Task CreateAsync(Loan entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Loan>> ReadAllAsync(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<Loan> ReadAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Loan entity)
        {
            throw new NotImplementedException();
        }
    }
}
