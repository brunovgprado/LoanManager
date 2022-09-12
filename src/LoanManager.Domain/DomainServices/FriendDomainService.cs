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

        #region CRUD operations
        public async Task<Guid> CreateAsync(Friend entity)
        {
            // Validating entity
            await _createFriendValidations.ValidateAndThrowAsync(entity);

            // Setting unique identificator to entity
            entity.Id = Guid.NewGuid();

            // Persisting entity
            await _unityOfWork.Friends.CreateAsync(entity);
            return entity.Id;
        }

        public async Task<IEnumerable<Friend>> ReadAllAsync(int offset, int limit)
        {
            return await _unityOfWork.Friends.ReadAllAsync(offset, limit);
        }

        public async Task<Friend> ReadAsync(Guid id)
        {
            // Verifying if friend exists on database
            var friendExistis = await this.VerifyIfFriendExsistsById(id);
            if (!friendExistis)
                throw new EntityNotExistsException();

            return await _unityOfWork.Friends.ReadAsync(id);
        }

        public async Task Update(Friend entity)
        {
            // Verifying if friend exists on database
            var friendExistis = await this.VerifyIfFriendExsistsById(entity.Id);
            if (!friendExistis)
                throw new EntityNotExistsException();

            await _unityOfWork.Friends.Update(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            // Verifying if friend exists on database
            var friendExistis = await this.VerifyIfFriendExsistsById(id);
            if (!friendExistis)
                throw new EntityNotExistsException();

            await _unityOfWork.Friends.DeleteAsync(id);
        }
        #endregion

        private async Task<bool> VerifyIfFriendExsistsById(Guid id)
        {
            return await _unityOfWork.Friends.VerifyIfFriendExsistsById(id);
        }
    }
}
