using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LoanManager.Api.Controller;
using LoanManager.Api.Helpers;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Application.Shared;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using LoanManager.Tests.Builders;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LoanManager.Tests.ApiControllers
{
    public class LoanControllerTest
    {
        private readonly Mock<ILoanAppService> _loanServiceMock;
        private readonly INotificationHandler _notificationHandler;
        private readonly LoanController _controller;
        private readonly IMapper _mapperMock;

        public LoanControllerTest()
        {
            _notificationHandler = new NotificationHandler();
            _loanServiceMock = new Mock<ILoanAppService>();
            if (_mapperMock is null)
            {
                var config = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                var mapper = config.CreateMapper();
                _mapperMock = mapper;
            }

            _controller = new LoanController(_notificationHandler, _loanServiceMock.Object, _mapperMock);
        }
        
        [Fact(DisplayName = "Success on getting")]
        [Trait("Get Loan with success", "Get")]
        public async Task GetLoan_WithValidData_MustResultOk()
        {
            //Arrange
            var id = Guid.NewGuid();
            var loan = LoanDtoMock.GenerateLoanDto();
            var response = new Response<LoanDto>();
            response.SetResult(loan);            

            _loanServiceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(response);
            
            //Act
            var actual = await _controller.Get(id);
            
            //Assert
            Assert.IsType<OkObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Success on getting list")]
        [Trait("Get Loan list with success", "Get")]
        public async Task GetLoanList_WithValidData_MustResultOk()
        {
            //Arrange
            const int offset = 1;
            const int limit = 20;
            const int mockQuantity = 20;
            
            var id = Guid.NewGuid();
            var loan = LoanDtoMock.GenerateLoanDto(mockQuantity);
            var response = new Response<IEnumerable<LoanDto>>();
            response.SetResult(loan);            

            _loanServiceMock
                .Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(response);
            
            //Act
            var actual = await _controller.Get(offset, limit);
            
            //Assert
            Assert.IsType<OkObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Fail on getting")]
        [Trait("Get nonexistent Loan must result Bad Request", "Get")]
        public async Task GetLoan_NonExistent_MustResultBadRequest()
        {
            //Arrange
            var id = Guid.NewGuid();
            _notificationHandler.AddNotification(new Notification(key:"NotFound", message:"Loan not found with given id"));

            //Act
            var actual = await _controller.Get(id);
            
            //Assert
            Assert.IsType<NotFoundObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Success on creating")]
        [Trait("Create Loan with success", "Create")]
        public async Task CreateLoan_WithValidData_MustResultOk()
        {
            //Arrange
            var request = CreateLoanRequestDtoMock.GenerateLoanRequestDto();

            var response = new Response<Guid>();
            response.SetResult(Guid.NewGuid());            

            _loanServiceMock.Setup(x => x.Create(It.IsAny<LoanDto>())).ReturnsAsync(response);
            
            //Act
            var actual = await _controller.Create(request);
            
            //Assert
            Assert.IsType<OkObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Fail on creating")]
        [Trait("Create Loan with empty friend id must result in Bad Request", "Create")]
        public async Task CreateLoan_WithEmptyName_MustResultBadRequest()
        {
            //Arrange
            var request = CreateLoanRequestDtoMock.GenerateLoanRequestDtoWithEmptyFriendId();
            
            _notificationHandler.AddNotification(new Notification(key: "InputValidation", message: "Friend id can't be empty"));

            //Act
            var actual = await _controller.Create(request);
            
            //Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Fact(DisplayName = "Success on deleting")]
        [Trait("Delete loan with success must return Ok Result", "Delete")]
        public async Task DeleteLoan_WithSuccess_MustReturnOk()
        {
            //Arrange
            var loanRequest = Guid.NewGuid();
            var result = new Response<bool>();
            result.SetResult(true);
            
            _loanServiceMock.Setup(l => l.Delete(It.IsAny<Guid>())).ReturnsAsync(result);
            
            //Act
            var actual = await _controller.Delete(loanRequest);
            
            //Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact(DisplayName = "Fail on deleting")]
        [Trait("Delete nonexistent Loan must return Not Found", "Delete")]
        public async Task DeleteLoan_NonExistent_MustResultNotFound()
        {
            //Arrange
            var loanRequest = Guid.NewGuid();
            _notificationHandler.AddNotification(key:"NotFound", message:"No Loan was found with given id");
            
            //Act
            var actual = await _controller.Delete(loanRequest);

            //Assert
            Assert.IsType<NotFoundObjectResult>(actual);
        }
        
                [Fact(DisplayName = "Fail on creating")]
        [Trait("Create Loan with Game already in loan must return Conflict", "Create")]
        public async Task CreateLoan_GameAlreadyInLoan_MustResultConflict()
        {
            //Arrange
            var request = CreateLoanRequestDtoMock.GenerateLoanRequestDto();
            _notificationHandler.AddNotification(key:"BusinessRule", message:"This Game is already in a Loan");
            
            //Act
            var actual = await _controller.Create(request);

            //Assert
            Assert.IsType<ConflictObjectResult>(actual);
        }
    }
}