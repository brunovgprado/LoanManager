using LoanManager.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface IGameRepository : IBaseRepository<Guid, Game>
    {
        Task<bool> CheckIfGameExistsById(Guid id);
    }
}
