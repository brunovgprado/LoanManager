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
    public class LoanController : BaseController
    {
        private readonly ILoanAppService _loanAppService;
        private readonly IMapper _mapper;

        public LoanController(
            INotificationHandler notificationHandler,
            ILoanAppService loanAppService,
            IMapper mapper): base(notificationHandler)
        {
            _loanAppService = loanAppService;
            _mapper = mapper;
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Create(CreateLoanRequestDto loan)
        {
            var loanDto = _mapper.Map<LoanDto>(loan);
            var result = await _loanAppService.Create(loanDto);
            return CreateResult(data: result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(LoanDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _loanAppService.Get(id);
            return CreateResult(data: result);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<LoanDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromQuery] int offset, int limit)
        {
            var result = await _loanAppService.Get(offset, limit);
            return CreateResult(data: result);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(LoanDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _loanAppService.Delete(id);
            return CreateResult(data: result);
        }

        [HttpGet("/loanhistory")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<LoanDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByGame([FromQuery] Guid id, int offset, int limit)
        {
            var result = await _loanAppService.ReadLoanHistoryByGameAsync(id, offset, limit);
            return CreateResult(data: result);
        }

        [HttpPut("/endloan")]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> EndLoan(Guid id)
        {
            var result = await _loanAppService.EndLoan(id);
            return CreateResult(data: result);
        }
    }
}
