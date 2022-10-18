using LoanManager.Application.Models.DTO;
using LoanManager.Application.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Application.Interfaces.AppServices
{
    public interface IFriendAppService
    {
        Task<Response<Guid>> CreateAsync(FriendDto friend);
        Task<Response<FriendDto>> Get(Guid id);
        Task<Response<IEnumerable<FriendDto>>> Get(int offset, int limit);
        Task<Response<bool>> Update(FriendDto friend);
        Task<Response<bool>> Async(Guid id);
    }
}
