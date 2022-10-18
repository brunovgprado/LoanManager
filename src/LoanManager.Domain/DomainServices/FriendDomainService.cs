using LoanManager.Domain.Entities;
using LoanManager.Domain.Exceptions;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Domain.Validators.FriendValidators;
using LoanManager.Infrastructure.CrossCutting.Helpers;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Domain.DomainServices
{
    public class FriendDomainService : BaseDomainService, IFriendDomainService
    {
        private readonly IUnitOfWork _unityOfWork;
        private readonly CreateFriendValidator _createFriendValidations;

        public FriendDomainService(
            CreateFriendValidator createFriendValidations,
            INotificationHandler notificationHandler,
            IFriendRepository friendRepository)
            :base(notificationHandler)
        {
            _unityOfWork = unityOfWork;
            _createFriendValidations = createFriendValidations;
        }
        
        public async Task<Guid> CreateAsync(Friend entity)
        {
            GuardClauses.IsNotNull(entity, nameof(entity));

            if(IsValid(entity, _createFriendValidations))
            {
                entity.Id = Guid.NewGuid();
                await _friendRepository.CreateAsync(entity);
            }
            
            return entity.Id;
        }

        public async Task<IEnumerable<Friend>> ReadAllAsync(int offset, int limit)
        {
            return await _unityOfWork.Friends.ReadAllAsync(offset, limit);
        }

        public async Task<Friend> ReadAsync(Guid id)
        {
            await this.CheckIfEntityExistsById(id);

            return await _unityOfWork.Friends.ReadAsync(id);
        }

        public async Task Update(Friend entity)
        {
            GuardClauses.IsNotNull(entity, nameof(entity));

            if(IsValid(entity, _createFriendValidations))
            {
                await this.CheckIfEntityExistsById(entity.Id);
                await _friendRepository.UpdateAsync(entity);
                return true;
            }

            return false;
        }

        public async Task DeleteAsync(Guid id)
        {
            await this.CheckIfEntityExistsById(id);

            await _unityOfWork.Friends.DeleteAsync(id);
        }

        private async Task<bool> CheckIfEntityExistsById(Guid id)
        {
            var entityExists = await _unityOfWork.Friends.CheckIfFriendExistsById(id);

            if (!entityExists)
            {
                notificationHandler
                    .AddNotification(new Notification("NotFound", $"Friend not found with given id {id}"));
                return false;
            }

            return true;
        }
    }
}
