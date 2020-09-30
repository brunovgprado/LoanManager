using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Infrastructure.DataAccess.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        public Task CreateAsync(Friend entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Friend>> ReadAllAsync(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<Friend> ReadAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Friend entity)
        {
            throw new NotImplementedException();
        }
    }
}
