using AutoMapper;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
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

        public async Task<Guid> Create(LoanDto loan)
        {
            var loanEntity = _mapper.Map<Loan>(loan);
            return await _loanDomainService.CreateAsync(loanEntity);
        }

        public async Task<LoanDto> Get(Guid id)
        {
            var result = await _loanDomainService.GetAsync(id);
            return _mapper.Map<LoanDto>(result);
        }

        public async Task<IEnumerable<LoanDto>> Get(int offset, int limit)
        {
            var result = await _loanDomainService.GetAsync(offset, limit);
            return _mapper.Map<IEnumerable<LoanDto>>(result);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _loanDomainService.DeleteAsync(id);
        }

        public async Task<bool> EndLoan(Guid id)
        {
            return await _loanDomainService.FinishLoanAsync(id);
        }

        public async Task<IEnumerable<LoanDto>> ReadLoanHistoryByGameAsync(Guid id, int offset, int limit)
        {
            var result = await _loanDomainService.ReadLoanHistoryByGameAsync(id, offset, limit);
            return _mapper.Map<IEnumerable<LoanDto>>(result);
        }
    }
}