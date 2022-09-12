using AutoMapper;
using LoanManager.Api.Models;
using LoanManager.Api.Models.Request;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LoanManager.Api.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class GamesController : ControllerBase
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly IGameAppService _gameService;
        private readonly IMapper _mapper;

        public GamesController(
            IActionResultConverter actionResultConverter,
            IGameAppService gameService,
            IMapper mapper)
        {
            _actionResultConverter = actionResultConverter;
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(PaginatedDto<PartnerDeliveryDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Create(CreateGameRequest game)
        {
            var gameDto = _mapper.Map<GameDto>(game);
            return _actionResultConverter.Convert( await _gameService.Create(gameDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(Guid id)
        {
            return _actionResultConverter.Convert(await _gameService.Get(id));
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll([FromQuery]int offset, int limit)
        {
            return _actionResultConverter.Convert(await _gameService.GetAll(offset, limit));
        }

        [HttpPut]
        public async Task<IActionResult> Update(GameDto game)
        {
            return _actionResultConverter.Convert(await _gameService.Update(game));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return _actionResultConverter.Convert(await _gameService.Delete(id));
        }
    }
}
