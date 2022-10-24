using AutoMapper;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Application.Shared;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<Guid> Create(GameDto game)
        {
            var gameEntity = _mapper.Map<Game>(game);
            return await _gameDomainService.CreateAsync(gameEntity);
        }

        public async Task<GameDto> Get(Guid id)
        {
            var result = await _gameDomainService.GetAsync(id);
            return _mapper.Map<GameDto>(result);
        }

        public async Task<IEnumerable<GameDto>> Get(int offset, int limit)
        {
            var result = await _gameDomainService.GetAsync(offset, limit);
            return _mapper.Map<IEnumerable<GameDto>>(result);
        }

        public async Task<bool> Update(GameDto game)
        {
            var gameEntity = _mapper.Map<Game>(game);

            return await _gameDomainService.UpdateAsync(gameEntity);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _gameDomainService.DeleteAsync(id);
        }
    }
}