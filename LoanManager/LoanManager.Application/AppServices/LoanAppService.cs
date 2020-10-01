using AutoMapper;
using FluentValidation;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Application.Properties;
using LoanManager.Application.Shared;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Validators.LoanValidators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Application.AppServices
{
    public class LoanAppService : ILoanAppService
    {
        private readonly ILoanDomainService _loanDomainService;
        private readonly CreateLoanValidator _createLoanValidations;
        private readonly IMapper _mapper;

        public LoanAppService(
            ILoanDomainService loanDomainService,
            CreateLoanValidator validations,
            IMapper mapper
            )
        {
            _loanDomainService = loanDomainService;
            _createLoanValidations = validations;
            _mapper = mapper;
        }

        #region CRUD operations
        public async Task<Response<object>> Create(LoanDto loan)
        {
            var response = new Response<Object>();
            try
            {
                var loanEntity = _mapper.Map<Loan>(loan);
                await _createLoanValidations.ValidateAndThrowAsync(loanEntity);
                var result = await _loanDomainService.CreateAsync(loanEntity);
                return response.SetResult(new { Id = result });
            }
            catch (ValidationException ex)
            {
                return response.SetRequestValidationError(ex);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGeneratingLoan);
            }
        }

        public async Task<Response<LoanDto>> Get(Guid id)
        {
            var response = new Response<LoanDto>();
            try
            {
                var result = await _loanDomainService.ReadAsync(id);
                return response.SetResult(_mapper.Map<LoanDto>(result));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGettingLoan);
            }
        }

        public async Task<Response<IEnumerable<LoanDto>>> GetAll(int offset, int limit)
        {
            var response = new Response<IEnumerable<LoanDto>>();
            try
            {
                var result = await _loanDomainService.ReadAllAsync(offset, limit);
                return response.SetResult(_mapper.Map<IEnumerable<LoanDto>>(result));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGettingLoan);
            }
        }

        public async Task<Response<bool>> Update(LoanDto game)
        {
            var response = new Response<bool>();
            try
            {
                var gameEntity = _mapper.Map<Loan>(game);
                _loanDomainService.Update(gameEntity);
                return response.SetResult(true);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileUpdatingLoan);
            }
        }

        public async Task<Response<bool>> Delete(Guid id)
        {
            var response = new Response<bool>();
            try
            {
                await _loanDomainService.DeleteAsync(id);
                return response.SetResult(true);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileDeletingLoan);
            }
        }
        #endregion

        public async Task<Response<IEnumerable<LoanDto>>> ReadLoanByFriendNameAsync(string name, int offset, int limit)
        {
            var response = new Response<IEnumerable<LoanDto>>();
            try
            {
                var result = await _loanDomainService.ReadLoanByFriendNameAsync(name, offset, limit);
                return response.SetResult(_mapper.Map<IEnumerable<LoanDto>>(result));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGettingLoan);
            }
        }

        public async Task<Response<bool>> EndLoan(Guid id)
        {
            var response = new Response<bool>();
            try
            {
                await _loanDomainService.EndLoan(id);
                return response.SetResult(true);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileDeletingLoan);
            }           
        }

        public async Task<Response<IEnumerable<LoanDto>>> ReadLoanHistoryByGameAsync(Guid id, int offset, int limit)
        {
            var response = new Response<IEnumerable<LoanDto>>();
            try
            {
                var result = await _loanDomainService.ReadLoanHistoryByGameAsync(id, offset, limit);
                return response.SetResult(_mapper.Map<IEnumerable<LoanDto>>(result));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGettingLoan);
            }
        }
    }
}
