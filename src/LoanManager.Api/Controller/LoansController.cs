using AutoMapper;
using LoanManager.Api.Models;
using LoanManager.Api.Models.Request;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Create(CreateLoanRequest loan)
        {
            var loanDto = _mapper.Map<LoanDto>(loan);
            return _actionResultConverter.Convert(await _loanAppService.Create(loanDto));
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(LoanDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Read(Guid id)
        {
            return _actionResultConverter.Convert(await _loanAppService.Get(id));
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<LoanDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ReadAll([FromQuery] int offset, int limit)
        {
            return _actionResultConverter.Convert(await _loanAppService.GetAll(offset, limit));
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(LoanDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return _actionResultConverter.Convert(await _loanAppService.Delete(id));
        }

        [HttpGet("/loansbygameid")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<LoanDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByGame([FromQuery] Guid id, int offset, int limit)
        {
            return _actionResultConverter.Convert(
                await _loanAppService.ReadLoanHistoryByGameAsync(id, offset, limit));
        }

        [HttpPut("/endloan")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> EndLoan(Guid id)
        {
            return _actionResultConverter.Convert(await _loanAppService.EndLoan(id));
        }
    }
}
