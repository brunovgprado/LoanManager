using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Domain.Interfaces.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Application.AppServices
{
    public class GameAppService : IGameAppService
    {
        private readonly IGameDomainService _gameDomainService;

        public GameAppService(IGameDomainService gameDomainService)
        {
            _gameDomainService = gameDomainService;
        }

        public void Create(GameDto game)
        {
            _gameDomainService.CreateAsync(game);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public GameDto Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameDto> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
