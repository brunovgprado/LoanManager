using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanManager.Api.Models;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LoanManager.Api.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly IGameAppService _gameService;

        public GamesController(
            IActionResultConverter actionResultConverter,
            IGameAppService gameService)
        {
            _actionResultConverter = actionResultConverter;
            _gameService = gameService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(GameDto game)
        {
            return _actionResultConverter.Convert( await _gameService.Create(game));
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
