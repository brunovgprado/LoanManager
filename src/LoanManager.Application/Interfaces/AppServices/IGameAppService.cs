
using LoanManager.Application.Models.DTO;
using LoanManager.Application.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Application.Interfaces.AppServices
{
    public interface IGameAppService
    {
        Task<Response<Guid>> Create(GameDto game);
        Task<Response<GameDto>> Get(Guid id);
        Task<Response<IEnumerable<GameDto>>> GetAll(int offset, int limit);
        Task<Response<bool>> Update(GameDto game);
        Task<Response<bool>> Delete(Guid id);
    }
}
