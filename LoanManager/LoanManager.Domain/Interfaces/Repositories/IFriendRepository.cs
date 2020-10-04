using LoanManager.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface IFriendRepository : IGenericRepository<Guid, Friend>
    {
        Task<bool> VerifyIfFriendExsistsById(Guid id);
    }
}
