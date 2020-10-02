using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoanManager.Domain.Validators.GameValidators;
using FluentValidation;

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
            return await _unityOfWork.Games.ReadAsync(id);
        }

        public void Update(Game entity)
        {
             _unityOfWork.Games.Update(entity);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _unityOfWork.Games.DeleteAsync(id);            
        }
        #endregion
    }
}
