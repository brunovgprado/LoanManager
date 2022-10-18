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
    public class GamesController : BaseController
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly IGameAppService _gameService;
        private readonly IMapper _mapper;

        public GamesController(
            IActionResultConverter actionResultConverter,
            INotificationHandler notificationHandler,
            IGameAppService gameService,
            IMapper mapper):base(notificationHandler)
        {
            _actionResultConverter = actionResultConverter;
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create(CreateGameRequest game)
        {
            var gameDto = _mapper.Map<GameDto>(game);
            var result = await _gameService.Create(gameDto);
            return CreateResult(data: result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(GameDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Read(Guid id)
        {
            var result = await _gameService.Get(id);
            return CreateResult(data: result);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<GameDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ReadAll([FromQuery]int offset, int limit)
        {
            var result = await _gameService.GetAll(offset, limit);
            return CreateResult(data: result);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(GameDto game)
        {
            var result = await _gameService.Update(game);
            return CreateResult(data: result);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _gameService.Delete(id);
            return CreateResult(data: result);
        }
    }
}
