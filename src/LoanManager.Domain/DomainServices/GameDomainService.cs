using FluentValidation;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Exceptions;
using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Validators.GameValidators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoanManager.Domain.Interfaces.Repositories;

namespace LoanManager.Domain.DomainServices
{
    public class GameDomainService : IGameDomainService
    {
        private readonly IGameRepository _gameRepository;
        private readonly CreateGameValidator _createGameValidator;


        public GameDomainService(
            CreateGameValidator createGameValidator, 
            IGameRepository gameRepository)
        {
            _createGameValidator = createGameValidator;
            _gameRepository = gameRepository;
        }
        
        public async Task<Guid> CreateAsync(Game game)
        {
            await _createGameValidator.ValidateAndThrowAsync(game);
            
            await _gameRepository.CreateAsync(game);
            return game.Id;
        }

        public async Task<IEnumerable<Game>> GetAsync(int offset, int limit)
        {
            return await _gameRepository.GetAsync(offset, limit);
        }

        public async Task<Game> GetAsync(Guid id)
        {
            return await _gameRepository.GetAsync(id);
        }

        public async Task<bool> UpdateAsync(Game entity)
        {
            var gameExists = await this.CheckIfGameExistsById(entity.Id);
            if (!gameExists)
                throw new EntityNotExistsException();

            return await _gameRepository.UpdateAsync(entity);
        }
        
        public async Task<bool> DeleteAsync(Guid id)
        {
            var gameExists = await this.CheckIfGameExistsById(id);
            if (!gameExists)
                throw new EntityNotExistsException();

            return await _gameRepository.DeleteAsync(id);
        }

        private async Task<bool> CheckIfGameExistsById(Guid id)
        {
            return await _gameRepository.CheckIfGameExistsById(id);
        }
    }
}
