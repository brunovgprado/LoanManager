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

        public async Task<Response<Guid>> Create(GameDto game)
        {
            var response = new Response<Guid>();
            var gameEntity = _mapper.Map<Game>(game);
            var result = await _gameDomainService.CreateAsync(gameEntity);
            return response.SetResult(result);
        }

        public async Task<Response<GameDto>> Get(Guid id)
        {
            var response = new Response<GameDto>();
            var result = await _gameDomainService.GetAsync(id);
            return response.SetResult(_mapper.Map<GameDto>(result));
        }

        public async Task<Response<IEnumerable<GameDto>>> Get(int offset, int limit)
        {
            var response = new Response<IEnumerable<GameDto>>();

            var result = await _gameDomainService.GetAsync(offset, limit);
            return response.SetResult(_mapper.Map<IEnumerable<GameDto>>(result));
        }

        public async Task<Response<bool>> Update(GameDto game)
        {
            var response = new Response<bool>();

            var gameEntity = _mapper.Map<Game>(game);

            await _gameDomainService.UpdateAsync(gameEntity);
            return response.SetResult(true);
        }

        public async Task<Response<bool>> Delete(Guid id)
        {
            var response = new Response<bool>();

            await _gameDomainService.DeleteAsync(id);
            return response.SetResult(true);
        }
    }
}