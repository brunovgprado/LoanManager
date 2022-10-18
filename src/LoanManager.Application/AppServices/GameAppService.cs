using AutoMapper;
using FluentValidation;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Application.Properties;
using LoanManager.Application.Shared;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Exceptions;
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
            try
            {
                var gameEntity = _mapper.Map<Game>(game);
                var result = await _gameDomainService.CreateAsync(gameEntity);
                return response.SetResult(result);
            }
            catch (ValidationException ex)
            {
                return response.SetRequestValidationError(ex);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileCreatingGame);
            }
        }


        public async Task<Response<GameDto>> Get(Guid id)
        {
            var response = new Response<GameDto>();
            try
            {
              var result = await  _gameDomainService.GetAsync(id);
              return response.SetResult( _mapper.Map<GameDto>(result));
            }
            catch (EntityNotExistsException)
            {
                return response.SetNotFound(Resources.CantFounGameWithGivenId);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGettingGame);
            }
        }

        public async Task<Response<IEnumerable<GameDto>>> GetAll(int offset, int limit)
        {
            var response = new Response<IEnumerable<GameDto>>();
            try
            {
                var result = await _gameDomainService.GetAsync(offset, limit);
                return response.SetResult(_mapper.Map<IEnumerable<GameDto>>(result));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGettingGame);
            }
        }

        public async Task<Response<bool>> Update(GameDto game)
        {            
            var response = new Response<bool>();
            try
            {
                // Mapping GameDto into Game entity
                var gameEntity = _mapper.Map<Game>(game);

                // Persisting entity and returning
                await _gameDomainService.UpdateAsync(gameEntity);
                return response.SetResult(true);
            }
            catch(EntityNotExistsException)
            {
                return response.SetNotFound(Resources.CantFounGameWithGivenId);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileUpdatingGame);
            }
        }
        public async Task<Response<bool>> Delete(Guid id)
        {
            var response = new Response<bool>();
            try
            {
                // Deleting game and returning
                await _gameDomainService.DeleteAsync(id);
                return response.SetResult(true);

            }
            catch (EntityNotExistsException)
            {
                return response.SetNotFound(Resources.CantFounGameWithGivenId);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileDeletingGame);
            }
        }
    }
}
