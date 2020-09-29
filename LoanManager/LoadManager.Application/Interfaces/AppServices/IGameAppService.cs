
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
        GameDto Get(Guid id);
        IEnumerable<GameDto> GetAll();
        void Delete(Guid id);
    }
}
