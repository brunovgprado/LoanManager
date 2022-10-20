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
        private readonly IGameRepository _gameRepository;
        private readonly CreateGameValidator _createGameValidator;


        public GameDomainService(
            CreateGameValidator createGameValidator,
            INotificationHandler notificationHandler,
            IGameRepository gameRepository)
            : base(notificationHandler)
        {
            _createGameValidator = createGameValidator;
            _gameRepository = gameRepository;
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
            GuardClauses.IsNotNull(entity, nameof(entity));

            var gameExists = await this.CheckIfGameExistsById(entity.Id);
            if (!gameExists)
                return false;

            return await _gameRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var gameExists = await this.CheckIfGameExistsById(id);
            if (!gameExists)
                return false;

            return await _gameRepository.DeleteAsync(id);
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
