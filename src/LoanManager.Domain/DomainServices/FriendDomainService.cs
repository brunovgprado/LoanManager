using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Domain.Properties;
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
        private readonly CreateFriendValidator _createFriendValidations;
        private readonly IFriendRepository _friendRepository;

        public FriendDomainService(
            CreateFriendValidator createFriendValidations,
            INotificationHandler notificationHandler,
            IFriendRepository friendRepository)
            :base(notificationHandler)
        {
            _createFriendValidations = createFriendValidations;
            _friendRepository = friendRepository;
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

        public async Task<IEnumerable<Friend>> GetAsync(int offset, int limit)
        {
            return await _friendRepository.GetAsync(offset, limit);
        }

        public async Task<Friend> GetAsync(Guid id)
        {
            var result =await _friendRepository.GetAsync(id);
            if(result is null)
                notificationHandler.AddNotification(new Notification("NotFound", Resources.CantFoundFriendWithGivenId));
            
            return result;
        }

        public async Task<bool> UpdateAsync(Friend entity)
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

        public async Task<bool> DeleteAsync(Guid id)
        {
            await this.CheckIfEntityExistsById(id);

            return await _friendRepository.DeleteAsync(id);
        }

        private async Task<bool> CheckIfEntityExistsById(Guid id)
        {
            var entityExists = await _friendRepository.CheckIfFriendExistsById(id);

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
