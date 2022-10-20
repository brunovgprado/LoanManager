using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Domain.Properties;
using LoanManager.Domain.Validators.LoanValidators;
using LoanManager.Infrastructure.CrossCutting.Helpers;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Domain.DomainServices
{
    public class LoanDomainService : BaseDomainService, ILoanDomainService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IFriendRepository _friendRepository;
        private readonly CreateLoanValidator _createLoanValidations;

        public LoanDomainService(
            CreateLoanValidator createLoanValidations,
            ILoanRepository loanRepository,
            IGameRepository gameRepository,
            IFriendRepository friendRepository,
            INotificationHandler notificationHandler)
            : base(notificationHandler)
        {
            _createLoanValidations = createLoanValidations;
            _loanRepository = loanRepository;
            _gameRepository = gameRepository;
            _friendRepository = friendRepository;
        }

        public async Task<Guid> CreateAsync(Loan entity)
        {
            GuardClauses.IsNotNull(entity, nameof(entity));

            if (IsValid(entity, _createLoanValidations))
            {
                if(!await CheckIfGameAndFriendExists(entity))
                    return entity.Id;

                var gameIsOnLoan = await _loanRepository
                    .CheckIfGameIsOnLoan(entity.GameId);

                if (gameIsOnLoan)
                {
                    notificationHandler
                        .AddNotification(new Notification("BusinessRule", "The given Game is already in loan"));
                    return entity.Id;
                }

                await _loanRepository.CreateAsync(entity);
            }

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
            GuardClauses.IsNotNull(entity, nameof(entity));

            var loanExists = await this.CheckIfLoanExistsById(entity.Id);
            if (!loanExists)
                return false;

            return await _loanRepository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var loanExists = await this.CheckIfLoanExistsById(id);
            if (!loanExists)
                return false;

            return await _loanRepository.DeleteAsync(id);
        }

        public async Task<bool> FinishLoanAsync(Guid id)
        {
            var loanExists = await this.CheckIfLoanExistsById(id);
            if (!loanExists)
                return false;

            return await _loanRepository.FinishLoanAsync(id);
        }

        public async Task<IEnumerable<Loan>> ReadLoanHistoryByGameAsync(Guid id, int offset, int limit)
        {
            return await _loanRepository.ReadLoanHistoryByGameAsync(id, offset, limit);
        }

        private async Task<bool> CheckIfLoanExistsById(Guid id)
        {
            var loanExists = await _loanRepository.CheckIfyIfLoanExistsById(id);
            if (!loanExists)
            {
                notificationHandler
                    .AddNotification("NotFound", $"Loan not found with given id {id}");
                return false;
            }
            return true;
        }

        private async Task<bool> CheckIfGameAndFriendExists(Loan loan)
        {
            var friendExists = await _friendRepository.CheckIfFriendExistsById(loan.FriendId);
            if (!friendExists)
                notificationHandler.AddNotification(new Notification("NotFound", Resources.CantFoundFriendWithGivenId));

            var gameExists = await _gameRepository.CheckIfGameExistsById(loan.GameId);
            if (!gameExists)
                notificationHandler.AddNotification(new Notification("NotFound", Resources.CantFounGameWithGivenId));

            if (friendExists && gameExists)
                return true;

            return false;
        }
    }
}
