using AutoMapper;
using FluentValidation;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Application.Properties;
using LoanManager.Application.Shared;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Validators.GameValidators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Application.AppServices
{
    public class GameAppService : IGameAppService
    {
        private readonly IGameDomainService _gameDomainService;
        private readonly CreateGameValidator _createGameValidator;
        private readonly IMapper _mapper;

        public GameAppService(
            IGameDomainService gameDomainService,
            CreateGameValidator createGameValidator,
            IMapper mapper
            )
        {
            _gameDomainService = gameDomainService;
            _createGameValidator = createGameValidator;
            _mapper = mapper;
        }

        public async Task<Response<Object>> Create(GameDto game)
        {
            var response = new Response<Object>();
            try
            {
                var gameEntity = _mapper.Map<Game>(game);
                await _createGameValidator.ValidateAndThrowAsync(gameEntity);
                var result = await _gameDomainService.CreateAsync(gameEntity);
                return response.SetResult(new { Id = result});
            }
            catch (ValidationException ex)
            {
                return response.SetRequestValidationError(ex);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorCreatingGame);
            }
        }

        public async Task<Response<bool>> Delete(Guid id)
        {
            var response = new Response<bool>();
            try
            {
                await _gameDomainService.DeleteAsync(id);
                return response.SetResult(true);
            }catch(Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorCreatingGame);
            }
        }

        public async Task<Response<GameDto>> Get(Guid id)
        {
            var response = new Response<GameDto>();
            try
            {
              var result = await  _gameDomainService.ReadAsync(id);
              return response.SetResult( _mapper.Map<GameDto>(result));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorCreatingGame);
            }
        }

        public async Task<Response<IEnumerable<GameDto>>> GetAll(int offset, int limit)
        {
            var response = new Response<IEnumerable<GameDto>>();
            try
            {
                var result = await _gameDomainService.ReadAllAsync(offset, limit);
                return response.SetResult(_mapper.Map<IEnumerable<GameDto>>(result));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorCreatingGame);
            }
        }

        public async Task<Response<bool>> Update(GameDto game)
        {            
            var response = new Response<bool>();
            try
            {
                var gameEntity = _mapper.Map<Game>(game);
                _gameDomainService.Update(gameEntity);
                return response.SetResult(true);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorCreatingGame);
            }
        }
    }
}
