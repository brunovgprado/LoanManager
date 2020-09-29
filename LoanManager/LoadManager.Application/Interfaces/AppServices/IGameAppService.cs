
using LoanManager.Application.Models.DTO;
using System;
using System.Collections.Generic;

namespace LoanManager.Application.Interfaces.AppServices
{
    public interface IGameAppService
    {
        void Create(GameDto game);
        GameDto Get(Guid id);
        IEnumerable<GameDto> GetAll();
        void Delete(Guid id);
    }
}
