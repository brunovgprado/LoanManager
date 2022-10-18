using AutoMapper;
using LoanManager.Api.Models;
using LoanManager.Api.Models.Request;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace LoanManager.Api.Controller
{
    [Authorize]
    public class FriendController : BaseController
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly IFriendAppService _friendService;
        private readonly IMapper _mapper;

        public FriendController(
            IActionResultConverter actionResultConverter,
            IFriendAppService friendService,
            INotificationHandler notificationHandler,
            IMapper mapper
            ):base(notificationHandler)
        {
            _actionResultConverter = actionResultConverter;
            _friendService = friendService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Create(CreateFriendRequest friend)
        {
            var friendDto = _mapper.Map<FriendDto>(friend);
            var result = await _friendService.Create(friendDto);
            return CreateResult(data: result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(FriendDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Read(Guid id)
        {
            var result = await _friendService.Get(id);
            return CreateResult(data: result);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<FriendDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ReadAll([FromQuery] int offset, int limit)
        {
            var result = await _friendService.GetAll(offset, limit);
            return CreateResult(data: result);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(FriendDto friend)
        {
            var result = await _friendService.Update(friend);
            return CreateResult(data: result);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _friendService.Delete(id);
            return CreateResult(data: result);
        }

    }
}
