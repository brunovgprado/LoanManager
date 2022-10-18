using AutoMapper;
using LoanManager.Api.Controller;
using LoanManager.Api.Helpers;
using LoanManager.Api.Models;
using LoanManager.Api.Models.Request;
using LoanManager.Api.Models.Response;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Application.Shared;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LoanManager.Tests.ApiControlers
{
    public class FriendControllerTest
    {
        private readonly Mock<IActionResultConverter> _actionResultConverterMock;
        private readonly Mock<IFriendAppService> _friendServiceMock;
        private readonly INotificationHandler _notificationHandler;
        private readonly FriendController _controller;
        private readonly IMapper _mapperMock;

        public FriendControllerTest()
        {
            _notificationHandler = new NotificationHandler();
            _actionResultConverterMock = new Mock<IActionResultConverter>();
            _friendServiceMock = new Mock<IFriendAppService>();
            if (_mapperMock is null)
            {
                var config = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                var mapper = config.CreateMapper();
                _mapperMock = mapper;
            }

            _controller = new FriendController(_actionResultConverterMock.Object, _friendServiceMock.Object, _notificationHandler, _mapperMock);
        }

        [Fact(DisplayName = "Success")]
        [Trait("Create Friend with success", "Create")]
        public async Task CreateFriend_WithValidData_MustResultOk()
        {
            //Arrange
            var request = new CreateFriendRequest
            {
                Name = "joao",
                PhoneNumber = "56215656564"
            };

            var response = new Response<Guid>();
            response.SetResult(Guid.NewGuid());            

            _friendServiceMock.Setup(x => x.Create(It.IsAny<FriendDto>())).ReturnsAsync(response);
            
            //Act
            var actual = await _controller.Create(request);
            
            //Assert
            Assert.IsType<OkObjectResult>(actual);
        }
    }
}