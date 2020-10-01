using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoanManager.Api.Models;
using LoanManager.Api.Models.Request;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanManager.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly IFriendAppService _friendService;
        private readonly IMapper _mapper;

        public FriendsController(
            IActionResultConverter actionResultConverter,
            IFriendAppService friendService,
            IMapper mapper
            )
        {
            _actionResultConverter = actionResultConverter;
            _friendService = friendService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFriendRequest friend)
        {
            var FriendDto = _mapper.Map<FriendDto>(friend);
            return _actionResultConverter.Convert(await _friendService.Create(FriendDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(Guid id)
        {
            return _actionResultConverter.Convert(await _friendService.Get(id));
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll([FromQuery] int offset, int limit)
        {
            return _actionResultConverter.Convert(await _friendService.GetAll(offset, limit));
        }

        [HttpPut]
        public async Task<IActionResult> Update(FriendDto friend)
        {
            return _actionResultConverter.Convert(await _friendService.Update(friend));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return _actionResultConverter.Convert(await _friendService.Delete(id));
        }

    }
}
