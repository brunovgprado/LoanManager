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
using System.Collections.Generic;
using System.Threading.Tasks;
using LoanManager.Tests.Builders;
using Xunit;

namespace LoanManager.Tests.ApiControllers
{
    public class FriendControllerTest
    {
        private readonly Mock<IFriendAppService> _friendServiceMock;
        private readonly INotificationHandler _notificationHandler;
        private readonly FriendController _controller;
        private readonly IMapper _mapperMock;

        public FriendControllerTest()
        {
            _notificationHandler = new NotificationHandler();
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

            _controller = new FriendController(_friendServiceMock.Object, _notificationHandler, _mapperMock);
        }

        [Fact(DisplayName = "Success on getting")]
        [Trait("Get Friend with success", "Get")]
        public async Task GetFriend_WithValidData_MustResultOk()
        {
            //Arrange
            var id = Guid.NewGuid();
            var friend = FriendDtoMock.GenerateFriend();
            var response = new Response<FriendDto>();
            response.SetResult(friend);            

            _friendServiceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(response);
            
            //Act
            var actual = await _controller.Get(id);
            
            //Assert
            Assert.IsType<OkObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Success on getting list")]
        [Trait("Get Friend list with success", "Get")]
        public async Task GetFriendList_WithValidData_MustResultOk()
        {
            //Arrange
            const int offset = 1;
            const int limit = 20;
            const int mockQuantity = 20;
            
            var id = Guid.NewGuid();
            var friend = FriendDtoMock.GenerateFriend(mockQuantity);
            var response = new Response<IEnumerable<FriendDto>>();
            response.SetResult(friend);            

            _friendServiceMock.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(response);
            
            //Act
            var actual = await _controller.Get(offset, limit);
            
            //Assert
            Assert.IsType<OkObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Fail on getting")]
        [Trait("Get nonexistent Friend must result Bad Request", "Get")]
        public async Task GetFriend_NonExistent_MustResultBadRequest()
        {
            //Arrange
            var id = Guid.NewGuid();
            _notificationHandler.AddNotification(new Notification(key:"NotFound", message:"Friend not found with given id"));

            //Act
            var actual = await _controller.Get(id);
            
            //Assert
            Assert.IsType<NotFoundObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Success on creating")]
        [Trait("Create Friend with success", "Create")]
        public async Task CreateFriend_WithValidData_MustResultOk()
        {
            //Arrange
            var request = CreateFriendRequestDtoMock.GenerateFriend();

            var response = new Response<Guid>();
            response.SetResult(Guid.NewGuid());            

            _friendServiceMock.Setup(x => x.CreateAsync(It.IsAny<FriendDto>())).ReturnsAsync(response);
            
            //Act
            var actual = await _controller.Create(request);
            
            //Assert
            Assert.IsType<OkObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Fail on creating")]
        [Trait("Create Friend with empty name must result in Bad Request", "Create")]
        public async Task CreateFriend_WithEmptyName_MustResultBadRequest()
        {
            //Arrange
            var request = CreateFriendRequestDtoMock.GenerateFriendWithNameEmpty();
            
            _notificationHandler.AddNotification(new Notification(key: "InputValidation", message: "Name can't be empty"));

            //Act
            var actual = await _controller.Create(request);
            
            //Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Success on updating")]
        [Trait("Update Friend with success", "Update")]
        public async Task UpdateFriend_WithValidData_MustResultOk()
        {
            //Arrange
            var request = FriendDtoMock.GenerateFriend();

            var response = new Response<bool>();
            response.SetResult(true);            

            _friendServiceMock.Setup(x => x.Update(It.IsAny<FriendDto>())).ReturnsAsync(response);
            
            //Act
            var actual = await _controller.Update(request);
            
            //Assert
            Assert.IsType<OkObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Fail on updating")]
        [Trait("Update nonexistent Friend must result in Not Found", "Update")]
        public async Task UpdateFriend_NonExistent_MustResultNotFound()
        {
            //Arrange
            var request = FriendDtoMock.GenerateFriend();
            _notificationHandler.AddNotification(new Notification(key: "NotFound", message: "Friend not found with given id"));

            //Act
            var actual = await _controller.Update(request);
            
            //Assert
            Assert.IsType<NotFoundObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Fail on deleting")]
        [Trait("Delete nonexistent Friend must result in Bad Request", "Delete")]
        public async Task DeleteFriend_NonExistent_MustResultNotFound()
        {
            //Arrange
            var request = Guid.NewGuid();
            _notificationHandler.AddNotification(new Notification(key: "NotFound", message: "Friend not found with given id"));

            //Act
            var actual = await _controller.Delete(request);
            
            //Assert
            Assert.IsType<NotFoundObjectResult>(actual);
        }
    }
}