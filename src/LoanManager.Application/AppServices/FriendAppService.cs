using AutoMapper;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Application.Shared;
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

        public async Task<Response<Guid>> CreateAsync(FriendDto friend)
        {
            GuardClauses.IsNotNull(friend, nameof(friend));

            var response = new Response<Guid>();

            var friendEntity = _mapper.Map<Friend>(friend);
            var result = await _friendDomainService.CreateAsync(friendEntity);
            return response.SetResult(result);
        }

        public async Task<Response<bool>> Async(Guid id)
        {
            var response = new Response<bool>();

            await _friendDomainService.DeleteAsync(id);
            return response.SetResult(true);
        }

        public async Task<Response<FriendDto>> Get(Guid id)
        {
            var response = new Response<FriendDto>();

            var result = await _friendDomainService.GetAsync(id);
            return response.SetResult(_mapper.Map<FriendDto>(result));
        }

        public async Task<Response<IEnumerable<FriendDto>>> Get(int offset, int limit)
        {
            var response = new Response<IEnumerable<FriendDto>>();

            var result = await _friendDomainService.GetAsync(offset, limit);
            return response.SetResult(_mapper.Map<IEnumerable<FriendDto>>(result));
        }

        public async Task<Response<bool>> Update(FriendDto friend)
        {
            var response = new Response<bool>();

            var friendEntity = _mapper.Map<Friend>(friend);
            await _friendDomainService.UpdateAsync(friendEntity);
            return response.SetResult(true);
        }
    }
}