using LoanManager.Application.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Application.Interfaces.AppServices
{
    public interface ILoanAppService
    {
        Task<Guid> Create(LoanDto loan);
        Task<LoanDto> Get(Guid id);
        Task<IEnumerable<LoanDto>> Get(int offset, int limit);
        Task<bool> Delete(Guid id);
        Task<bool> EndLoan(Guid id);
        Task<IEnumerable<LoanDto>> ReadLoanHistoryByGameAsync(Guid id, int offset, int limit);
    }
}
