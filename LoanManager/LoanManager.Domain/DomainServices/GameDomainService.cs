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

        #region CRUD operations
        public async Task<Guid> CreateAsync(Game game)
        {
            // Validantig entity
            await _createGameValidator.ValidateAndThrowAsync(game);

            // Setting unique identificatior to entity
            game.Id = Guid.NewGuid();

            // Persisting entity
            await _unityOfWork.Games.CreateAsync(game);
            return game.Id;
        }

        public async Task<IEnumerable<Game>> ReadAllAsync(int offset, int limit)
        {
            return await _unityOfWork.Games.ReadAllAsync(offset, limit);
        }

        public async Task<Game> ReadAsync(Guid id)
        {
            // Verifying if game exists on database
            var result = await _unityOfWork.Games.ReadAsync(id);
            if(result == null)
                throw new EntityNotExistsException();

            return result;
        }

        public async Task Update(Game entity)
        {
            // Verifying if game exists on database
            var gameExistis = await this.VerifyIfGameExsistsById(entity.Id);
            if (!gameExistis)
                throw new EntityNotExistsException();

            await _unityOfWork.Games.Update(entity);
        }
        public async Task DeleteAsync(Guid id)
        {
            // Verifying if friend exists on database
            var gameExistis = await this.VerifyIfGameExsistsById(id);
            if (!gameExistis)
                throw new EntityNotExistsException();

            await _unityOfWork.Games.DeleteAsync(id);
        }
        #endregion

        private async Task<bool> VerifyIfGameExsistsById(Guid id)
        {
            return await _unityOfWork.Games.VerifyIfGameExsistsById(id);
        }
    }
}
