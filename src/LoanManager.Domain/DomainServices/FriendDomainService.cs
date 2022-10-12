using FluentValidation;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Exceptions;
using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Validators.FriendValidators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Domain.DomainServices
{
    public class FriendDomainService : IFriendDomainService
    {
        private readonly IUnitOfWork _unityOfWork;
        private readonly CreateFriendValidator _createFriendValidations;

        public FriendDomainService(
            IUnitOfWork unityOfWork,
            CreateFriendValidator createFriendValidations
            )
        {
            _unityOfWork = unityOfWork;
            _createFriendValidations = createFriendValidations;
        }
        
        public async Task<Guid> CreateAsync(Friend entity)
        {
            await _createFriendValidations.ValidateAndThrowAsync(entity);
            
            entity.Id = Guid.NewGuid();
            await _unityOfWork.Friends.CreateAsync(entity);
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
            await this.CheckIfEntityExistsById(entity.Id);

            await _unityOfWork.Friends.Update(entity);
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
                throw new EntityNotExistsException();

            return true;
        }
    }
}
