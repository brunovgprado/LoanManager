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
        
        public async Task<Guid> CreateAsync(Loan entity)
        {
            await _createLoanValidations.ValidateAndThrowAsync(entity);
            
            await VerifyIfGameAndFriendExists(entity);
            
            var gameIsOnLoan = await _unityOfWork.Loans
                .CheckIfGameIsOnLoan(entity.GameId);
            
            if (gameIsOnLoan)
                throw new GameIsOnLoanException();
            
            entity.Id = Guid.NewGuid();
            entity.LoanDate = DateTime.Now;
            
            await _unityOfWork.Loans.CreateAsync(entity);
            return entity.Id;
        }

        public async Task<IEnumerable<Loan>> ReadAllAsync(int offset, int limit)
        {
            return await _unityOfWork.Loans.ReadAllAsync(offset, limit);
        }
        public async Task<Loan> ReadAsync(Guid id)
        {
            var result = await _unityOfWork.Loans.ReadAsync(id);
            if(result == null)
                throw new EntityNotExistsException();

            return result;
        }
        public async Task Update(Loan entity)
        {
            var loanExists = await this.CheckIfLoanExistsById(entity.Id);
            if (!loanExists)
                throw new EntityNotExistsException();

            await _unityOfWork.Loans.Update(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var loanExists = await this.CheckIfLoanExistsById(id);
            if (!loanExists)
                throw new EntityNotExistsException();

            await _unityOfWork.Loans.DeleteAsync(id);
        }
        
        public async Task EndLoan(Guid id)
        {
            var loanExists = await this.CheckIfLoanExistsById(id);
            if (!loanExists)
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

        private async Task<bool> CheckIfLoanExistsById(Guid id)
        {
            return await _unityOfWork.Loans.CheckIfyIfLoanExistsById(id);
        }

        private async Task VerifyIfGameAndFriendExists(Loan loan)
        {
            StringBuilder errorMessages = new StringBuilder();
            var friendExists = await _unityOfWork.Friends.CheckIfFriendExistsById(loan.FriendId);
            if (!friendExists)
                errorMessages.AppendLine(Resources.CantFounFriendWithGivenId);
            
            var gameExists = await _unityOfWork.Games.CheckIfGameExistsById(loan.GameId);
            if (!gameExists)
                errorMessages.AppendLine(Resources.CantFounGameWithGivenId);

            if (!friendExists || !gameExists)
                throw new EntityNotExistsException(errorMessages.ToString());
        }
    }
}
