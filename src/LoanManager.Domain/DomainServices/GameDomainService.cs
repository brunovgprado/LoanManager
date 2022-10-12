using FluentValidation;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Exceptions;
using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Validators.GameValidators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Domain.DomainServices
{
    public class GameDomainService : IGameDomainService
    {
        private readonly IUnitOfWork _unityOfWork;
        private readonly CreateGameValidator _createGameValidator;


        public GameDomainService(
            IUnitOfWork unityOfWork,
            CreateGameValidator createGameValidator
            )
        {
            _unityOfWork = unityOfWork;
            _createGameValidator = createGameValidator;
        }
        
        public async Task<Guid> CreateAsync(Game game)
        {
            await _createGameValidator.ValidateAndThrowAsync(game);
            
            game.Id = Guid.NewGuid();
            
            await _unityOfWork.Games.CreateAsync(game);
            return game.Id;
        }

        public async Task<IEnumerable<Game>> ReadAllAsync(int offset, int limit)
        {
            return await _unityOfWork.Games.ReadAllAsync(offset, limit);
        }

        public async Task<Game> ReadAsync(Guid id)
        {
            var result = await _unityOfWork.Games.ReadAsync(id);
            if(result == null)
                throw new EntityNotExistsException();

            return result;
        }

        public async Task Update(Game entity)
        {
            var gameExists = await this.CheckIfGameExistsById(entity.Id);
            if (!gameExists)
                throw new EntityNotExistsException();

            await _unityOfWork.Games.Update(entity);
        }
        public async Task DeleteAsync(Guid id)
        {
            var gameExists = await this.CheckIfGameExistsById(id);
            if (!gameExists)
                throw new EntityNotExistsException();

            await _unityOfWork.Games.DeleteAsync(id);
        }

        private async Task<bool> CheckIfGameExistsById(Guid id)
        {
            return await _unityOfWork.Games.CheckIfGameExistsById(id);
        }
    }
}
