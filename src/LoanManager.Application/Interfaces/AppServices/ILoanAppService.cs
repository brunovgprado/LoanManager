using LoanManager.Application.Models.DTO;
using LoanManager.Application.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Application.Interfaces.AppServices
{
    public interface ILoanAppService
    {
        Task<Response<Guid>> Create(LoanDto loan);
        Task<Response<LoanDto>> Get(Guid id);
        Task<Response<IEnumerable<LoanDto>>> GetAll(int offset, int limit);
        Task<Response<bool>> Delete(Guid id);
        Task<Response<IEnumerable<LoanDto>>> ReadLoanByFriendNameAsync(string name, int offset, int limit);
        Task<Response<bool>> EndLoan(Guid id);
        Task<Response<IEnumerable<LoanDto>>> ReadLoanHistoryByGameAsync(Guid id, int offset, int limit);
    }
}
