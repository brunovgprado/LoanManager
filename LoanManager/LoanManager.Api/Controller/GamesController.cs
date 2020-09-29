using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanManager.Api.Models;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using Microsoft.AspNetCore.Http;
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
    }
}
