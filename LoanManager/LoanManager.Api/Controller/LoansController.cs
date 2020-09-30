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
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly ILoanAppService _loanAppService;

        public LoansController(
            IActionResultConverter actionResultConverter,
            ILoanAppService loanAppService)
        {
            _actionResultConverter = actionResultConverter;
            _loanAppService = loanAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoanDto loan)
        {
            return _actionResultConverter.Convert(await _loanAppService.Create(loan));
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

        [HttpPut]
        public async Task<IActionResult> Update(LoanDto loan)
        {
            return _actionResultConverter.Convert(await _loanAppService.Update(loan));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return _actionResultConverter.Convert(await _loanAppService.Delete(id));
        }
    }
}
