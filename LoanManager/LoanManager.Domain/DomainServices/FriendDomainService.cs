using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LoanManager.Domain.Validators.FriendValidators;
using FluentValidation;

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
            return await _unityOfWork.Friends.ReadAsync(id);

        }

        public void Update(Friend entity)
        {
            _unityOfWork.Friends.Update(entity);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _unityOfWork.Games.DeleteAsync(id);
        }
        #endregion
    }
}
