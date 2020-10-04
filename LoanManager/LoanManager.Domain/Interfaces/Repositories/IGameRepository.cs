using LoanManager.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface IGameRepository : IGenericRepository<Guid, Game>
    {
        Task<bool> VerifyIfGameExsistsById(Guid id);
    }
}
