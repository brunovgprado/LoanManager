using AutoMapper;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Application.Shared;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Application.AppServices
{
    public class LoanAppService : ILoanAppService
    {
        private readonly ILoanDomainService _loanDomainService;
        private readonly IMapper _mapper;

        public LoanAppService(
            ILoanDomainService loanDomainService,
            IMapper mapper
        )
        {
            _loanDomainService = loanDomainService;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Create(LoanDto loan)
        {
            var response = new Response<Guid>();

            var loanEntity = _mapper.Map<Loan>(loan);
            var result = await _loanDomainService.CreateAsync(loanEntity);
            return response.SetResult(result);
        }

        public async Task<Response<LoanDto>> Get(Guid id)
        {
            var response = new Response<LoanDto>();

            var result = await _loanDomainService.GetAsync(id);
            return response.SetResult(_mapper.Map<LoanDto>(result));
        }

        public async Task<Response<IEnumerable<LoanDto>>> Get(int offset, int limit)
        {
            var response = new Response<IEnumerable<LoanDto>>();

            var result = await _loanDomainService.GetAsync(offset, limit);
            return response.SetResult(_mapper.Map<IEnumerable<LoanDto>>(result));
        }

        public async Task<Response<bool>> Delete(Guid id)
        {
            var response = new Response<bool>();

            await _loanDomainService.DeleteAsync(id);
            return response.SetResult(true);
        }

        public async Task<Response<bool>> EndLoan(Guid id)
        {
            var response = new Response<bool>();

            await _loanDomainService.FinishLoanAsync(id);
            return response.SetResult(true);
        }

        public async Task<Response<IEnumerable<LoanDto>>> ReadLoanHistoryByGameAsync(Guid id, int offset, int limit)
        {
            var response = new Response<IEnumerable<LoanDto>>();

            var result = await _loanDomainService.ReadLoanHistoryByGameAsync(id, offset, limit);
            return response.SetResult(_mapper.Map<IEnumerable<LoanDto>>(result));
        }
    }
}