using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Domain.Validators.GameValidators;
using LoanManager.Infrastructure.CrossCutting.Helpers;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Domain.DomainServices
{
    public class GameDomainService : BaseDomainService, IGameDomainService
    {
        private readonly IUnitOfWork _unityOfWork;
        private readonly CreateGameValidator _createGameValidator;


        public GameDomainService(
            CreateGameValidator createGameValidator,
            INotificationHandler notificationHandler,
            IGameRepository gameRepository)
            : base(notificationHandler)
        {
            _unityOfWork = unityOfWork;
            _createGameValidator = createGameValidator;
        }

        public async Task<Guid> CreateAsync(Game entity)
        {
            GuardClauses.IsNotNull(entity, nameof(entity));

            if (IsValid(entity, _createGameValidator))
            {
                await _gameRepository.CreateAsync(entity);
            }

            return entity.Id;
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
            GuardClauses.IsNotNull(entity, nameof(entity));

            var gameExists = await this.CheckIfGameExistsById(entity.Id);
            if (!gameExists)
                return false;

            await _unityOfWork.Games.Update(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var gameExists = await this.CheckIfGameExistsById(id);
            if (!gameExists)
                return false;

            await _unityOfWork.Games.DeleteAsync(id);
        }

        private async Task<bool> CheckIfGameExistsById(Guid id)
        {
            var gameExists = await _gameRepository.CheckIfGameExistsById(id);
            if (!gameExists)
            {
                notificationHandler
                    .AddNotification(new Notification("NotFound", $"Game not found with given id {id}"));
                return false;
            }

            return true;
        }
    }
}
