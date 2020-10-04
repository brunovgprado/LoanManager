using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoanManager.Api.Models;
using LoanManager.Api.Models.Request;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanManager.Api.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class LoansController : ControllerBase
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly ILoanAppService _loanAppService;
        private readonly IMapper _mapper;

        public LoansController(
            IActionResultConverter actionResultConverter,
            ILoanAppService loanAppService,
            IMapper mapper)
        {
            _actionResultConverter = actionResultConverter;
            _loanAppService = loanAppService;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateLoanRequest loan)
        {
            var loanDto = _mapper.Map<LoanDto>(loan);
            return _actionResultConverter.Convert(await _loanAppService.Create(loanDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(Guid id)
        {
            return _actionResultConverter.Convert(await _loanAppService.Get(id));
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll([FromQuery] int offset, int limit)
        {
            return _actionResultConverter.Convert(await _loanAppService.GetAll(offset, limit));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return _actionResultConverter.Convert(await _loanAppService.Delete(id));
        }

        [HttpGet("/friendloans")]
        public async Task<IActionResult> GetByFriendName([FromQuery] string friendName, int offset, int limit)
        {
            return _actionResultConverter.Convert(
                await _loanAppService.ReadLoanByFriendNameAsync(friendName, offset, limit));
        }

        [HttpGet("/loansbygameid")]
        public async Task<IActionResult> GetByGame([FromQuery] Guid id, int offset, int limit)
        {
            return _actionResultConverter.Convert(
                await _loanAppService.ReadLoanHistoryByGameAsync(id, offset, limit));
        }

        [HttpPut("/endloan")]
        public async Task<IActionResult> EndLoan(Guid id)
        {
            return _actionResultConverter.Convert(await _loanAppService.EndLoan(id));
        }
    }
}
