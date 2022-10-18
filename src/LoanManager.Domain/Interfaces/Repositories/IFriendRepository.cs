using LoanManager.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface IFriendRepository :  IBaseRepository<Guid, Friend>
    {
        Task<bool> CheckIfFriendExistsById(Guid id);
    }
}
