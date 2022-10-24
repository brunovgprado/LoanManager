
using LoanManager.Application.Models.DTO;
using LoanManager.Application.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Application.Interfaces.AppServices
{
    public interface IGameAppService
    {
        Task<Guid> Create(GameDto game);
        Task<GameDto> Get(Guid id);
        Task<IEnumerable<GameDto>> Get(int offset, int limit);
        Task<bool> Update(GameDto game);
        Task<bool> Delete(Guid id);
    }
}
