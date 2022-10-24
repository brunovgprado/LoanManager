using LoanManager.Application.Models.DTO;
using LoanManager.Application.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Application.Interfaces.AppServices
{
    public interface IFriendAppService
    {
        Task<Guid> CreateAsync(FriendDto friend);
        Task<FriendDto> Get(Guid id);
        Task<IEnumerable<FriendDto>> Get(int offset, int limit);
        Task<bool> Update(FriendDto friend);
        Task<bool> Async(Guid id);
    }
}
