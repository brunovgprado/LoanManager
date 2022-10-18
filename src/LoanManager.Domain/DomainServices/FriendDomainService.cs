using FluentValidation;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Exceptions;
using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Validators.FriendValidators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoanManager.Domain.Interfaces.Repositories;

namespace LoanManager.Domain.DomainServices
{
    public class FriendDomainService : IFriendDomainService
    {
        private readonly CreateFriendValidator _createFriendValidations;
        private readonly IFriendRepository _friendRepository;

        public FriendDomainService(
            CreateFriendValidator createFriendValidations, 
            IFriendRepository friendRepository)
        {
            _createFriendValidations = createFriendValidations;
            _friendRepository = friendRepository;
        }
        
        public async Task<Guid> CreateAsync(Friend entity)
        {
            await _createFriendValidations.ValidateAndThrowAsync(entity);
            
            entity.Id = Guid.NewGuid();
            await _friendRepository.CreateAsync(entity);
            return entity.Id;
        }

        public async Task<IEnumerable<Friend>> GetAsync(int offset, int limit)
        {
            return await _friendRepository.GetAsync(offset, limit);
        }

        public async Task<Friend> GetAsync(Guid id)
        {
            return await _friendRepository.GetAsync(id);
        }

        public async Task<bool> UpdateAsync(Friend entity)
        {
            await this.CheckIfEntityExistsById(entity.Id);

            return await _friendRepository.UpdateAsync(entity);
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
                throw new EntityNotExistsException();

            return true;
        }
    }
}
