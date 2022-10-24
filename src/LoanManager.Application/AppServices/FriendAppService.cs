using AutoMapper;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Infrastructure.CrossCutting.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Application.AppServices
{
    public class FriendAppService : IFriendAppService
    {
        private readonly IFriendDomainService _friendDomainService;
        private readonly IMapper _mapper;

        public FriendAppService(
            IFriendDomainService friendDomainService,
            IMapper mapper)
        {
            _friendDomainService = friendDomainService;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(FriendDto friend)
        {
            GuardClauses.IsNotNull(friend, nameof(friend));


            var friendEntity = _mapper.Map<Friend>(friend);
            return await _friendDomainService.CreateAsync(friendEntity);
        }

        public async Task<bool> Async(Guid id)
        {
            return await _friendDomainService.DeleteAsync(id);
        }

        public async Task<FriendDto> Get(Guid id)
        {
            var result = await _friendDomainService.GetAsync(id);
            return _mapper.Map<FriendDto>(result);
        }

        public async Task<IEnumerable<FriendDto>> Get(int offset, int limit)
        {
            var result = await _friendDomainService.GetAsync(offset, limit);
            return _mapper.Map<IEnumerable<FriendDto>>(result);
        }

        public async Task<bool> Update(FriendDto friend)
        {
            var friendEntity = _mapper.Map<Friend>(friend);
            return await _friendDomainService.UpdateAsync(friendEntity);
        }
    }
}