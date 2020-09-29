using AutoMapper;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Application.AppServices
{
    public class GameAppService : IGameAppService
    {
        private readonly IGameDomainService _gameDomainService;
        private readonly IMapper _mapper;

        public GameAppService(
            IGameDomainService gameDomainService,
            IMapper mapper
            )
        {
            _gameDomainService = gameDomainService;
            _mapper = mapper;
        }

        public void Create(GameDto game)
        {

            _gameDomainService.CreateAsync(_mapper.Map<Game>(game));
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
