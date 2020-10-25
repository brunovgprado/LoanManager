using FluentValidation;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Exceptions;
using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Properties;
using LoanManager.Domain.Validators.LoanValidators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Domain.DomainServices
{
    public class LoanDomainService : ILoanDomainService
    {
        private readonly IUnitOfWork _unityOfWork;
        private readonly CreateLoanValidator _createLoanValidations;

        public LoanDomainService(
            IUnitOfWork unityOfWork,
            CreateLoanValidator createLoanValidations
            )
        {
            _unityOfWork = unityOfWork;
            _createLoanValidations = createLoanValidations;
        }

        #region CRUD operations
        public async Task<Guid> CreateAsync(Loan entity)
        {
            // Validating entity properties
            await _createLoanValidations.ValidateAndThrowAsync(entity);

            // Verifyif friend and game exists on database and throw exception if not
            await VerifyIfGameAndFriendExists(entity);

            // Checks whether the game is on a loan in progress
            var gameIsOnLoan = await _unityOfWork.Loans
                .CheckIfGameIsOnALoanInProgress(entity.GameId);

            // Throwing exception when existis some user account with the same email adress
            if (gameIsOnLoan)
                throw new GameIsOnLoanException();

            // Seting date and unique identificator to entity before persit
            entity.Id = Guid.NewGuid();
            entity.LoanDate = DateTime.Now;

            // Persisting entity
            await _unityOfWork.Loans.CreateAsync(entity);
            return entity.Id;
        }

        public async Task<IEnumerable<Loan>> ReadAllAsync(int offset, int limit)
        {
            return await _unityOfWork.Loans.ReadAllAsync(offset, limit);
        }
        public async Task<Loan> ReadAsync(Guid id)
        {
            // Verifying if friend exists on database
            var result = await _unityOfWork.Loans.ReadAsync(id);
            if(result == null)
                throw new EntityNotExistsException();

            return result;
        }
        public async Task Update(Loan entity)
        {
            // Verifying if friend exists on database
            var loanExistis = await this.VerifyIfLoanExsistsById(entity.Id);
            if (!loanExistis)
                throw new EntityNotExistsException();

            await _unityOfWork.Loans.Update(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            // Verifying if loan exists on database
            var loanExistis = await this.VerifyIfLoanExsistsById(id);
            if (!loanExistis)
                throw new EntityNotExistsException();

            await _unityOfWork.Loans.DeleteAsync(id);
        }
        #endregion

        #region Business operations
        public async Task EndLoan(Guid id)
        {
            // Verifying if loan exists on database
            var loanExistis = await this.VerifyIfLoanExsistsById(id);
            if (!loanExistis)
                throw new EntityNotExistsException();

            await _unityOfWork.Loans.EndLoan(id);
        }

        public async Task<IEnumerable<Loan>> ReadLoanByFriendNameAsync(string name, int offset, int limit)
        {            
            return await _unityOfWork.Loans.ReadLoanByFriendNameAsync(name, offset, limit);
        }

        public async Task<IEnumerable<Loan>> ReadLoanHistoryByGameAsync(Guid id, int offset, int limit)
        {
            return await _unityOfWork.Loans.ReadLoanHistoryByGameAsync(id, offset, limit);
        }
        #endregion

        private async Task<bool> VerifyIfLoanExsistsById(Guid id)
        {
            return await _unityOfWork.Loans.VerifyIfLoanExsistsById(id);
        }

        private async Task VerifyIfGameAndFriendExists(Loan loan)
        {
            // Verifying if friend exists on database
            StringBuilder errorMessages = new StringBuilder();
            var friendExistis = await _unityOfWork.Friends.VerifyIfFriendExsistsById(loan.FriendId);
            if (!friendExistis)
                errorMessages.AppendLine(Resources.CantFounFriendWithGivenId);

            // Verifying if game exists on database
            var gameExistis = await _unityOfWork.Games.VerifyIfGameExsistsById(loan.GameId);
            if (!gameExistis)
                errorMessages.AppendLine(Resources.CantFounGameWithGivenId);

            if (!friendExistis || !gameExistis)
                throw new EntityNotExistsException(errorMessages.ToString());
        }
    }
}
