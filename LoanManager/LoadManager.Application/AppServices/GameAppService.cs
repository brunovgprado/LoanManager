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

        public async Task<Response<Guid>> Create(GameDto game)
        {
            var response = new Response<Guid>();
            try
            {
                var gameEntity = _mapper.Map<Game>(game);
                await _createGameValidator.ValidateAndThrowAsync(gameEntity);
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
                return response.SetInternalServerError(Resources.UnexpectedErrorCreatingGame);
            }
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
