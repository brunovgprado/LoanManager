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
using LoanManager.Domain.Interfaces.Repositories;

namespace LoanManager.Domain.DomainServices
{
    public class LoanDomainService : ILoanDomainService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IFriendRepository _friendRepository;
        private readonly CreateLoanValidator _createLoanValidations;

        public LoanDomainService(
            CreateLoanValidator createLoanValidations, 
            ILoanRepository loanRepository, 
            IGameRepository gameRepository, 
            IFriendRepository friendRepository)
        {
            _createLoanValidations = createLoanValidations;
            _loanRepository = loanRepository;
            _gameRepository = gameRepository;
            _friendRepository = friendRepository;
        }
        
        public async Task<Guid> CreateAsync(Loan entity)
        {
            await _createLoanValidations.ValidateAndThrowAsync(entity);
            
            await VerifyIfGameAndFriendExists(entity);
            
            var gameIsOnLoan = await _loanRepository
                .CheckIfGameIsOnLoan(entity.GameId);
            
            if (gameIsOnLoan)
                throw new GameIsOnLoanException();

            await _loanRepository.CreateAsync(entity);
            return entity.Id;
        }

        public async Task<IEnumerable<Loan>> GetAsync(int offset, int limit)
        {
            return await _loanRepository.GetAsync(offset, limit);
        }
        
        public async Task<Loan> GetAsync(Guid id)
        {
            return await _loanRepository.GetAsync(id);
        }
        
        public async Task<bool> UpdateAsync(Loan entity)
        {
            var loanExists = await this.CheckIfLoanExistsById(entity.Id);
            if (!loanExists)
                throw new EntityNotExistsException();

            return await _loanRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var loanExists = await this.CheckIfLoanExistsById(id);
            if (!loanExists)
                throw new EntityNotExistsException();

            return await _loanRepository.DeleteAsync(id);
        }
        
        public async Task<bool> FinishLoanAsync(Guid id)
        {
            var loanExists = await this.CheckIfLoanExistsById(id);
            if (!loanExists)
                throw new EntityNotExistsException();

            return await _loanRepository.FinishLoanAsync(id);
        }

        public async Task<IEnumerable<Loan>> ReadLoanHistoryByGameAsync(Guid id, int offset, int limit)
        {
            return await _loanRepository.ReadLoanHistoryByGameAsync(id, offset, limit);
        }

        private async Task<bool> CheckIfLoanExistsById(Guid id)
        {
            return await _loanRepository.CheckIfyIfLoanExistsById(id);
        }

        private async Task VerifyIfGameAndFriendExists(Loan loan)
        {
            var errorMessages = new StringBuilder();
            var friendExists = await _friendRepository.CheckIfFriendExistsById(loan.FriendId);
            if (!friendExists)
                errorMessages.AppendLine(Resources.CantFounFriendWithGivenId);
            
            var gameExists = await _gameRepository.CheckIfGameExistsById(loan.GameId);
            if (!gameExists)
                errorMessages.AppendLine(Resources.CantFounGameWithGivenId);

            if (!friendExists || !gameExists)
                throw new EntityNotExistsException(errorMessages.ToString());
        }
    }
}
