using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LoanManager.Domain.Validators.LoanValidators;
using FluentValidation;

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
            // Validating entity
            await _createLoanValidations.ValidateAndThrowAsync(entity);

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
            return await _unityOfWork.Loans.ReadAsync(id);
        }
        public void Update(Loan entity)
        {
            _unityOfWork.Loans.Update(entity);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _unityOfWork.Loans.DeleteAsync(id);
        }
        #endregion

        #region Business operations
        public async Task EndLoan(Guid id)
        {
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
    }
}
