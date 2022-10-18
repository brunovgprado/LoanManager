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
    public class GameControllerTest
    {
        private readonly Mock<IGameAppService> _gameServiceMock;
        private readonly INotificationHandler _notificationHandler;
        private readonly GameController _controller;
        private readonly IMapper _mapperMock;

        public GameControllerTest()
        {
            _notificationHandler = new NotificationHandler();
            _gameServiceMock = new Mock<IGameAppService>();
            if (_mapperMock is null)
            {
                var config = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AutoMapperProfile());
                });
                var mapper = config.CreateMapper();
                _mapperMock = mapper;
            }

            _controller = new GameController(_notificationHandler, _gameServiceMock.Object, _mapperMock);
        }
        
        [Fact(DisplayName = "Success on getting")]
        [Trait("Get Game with success", "Get")]
        public async Task GetGame_WithValidData_MustResultOk()
        {
            //Arrange
            var id = Guid.NewGuid();
            var game = GameDtoMock.GenerateGameDto();
            var response = new Response<GameDto>();
            response.SetResult(game);            

            _gameServiceMock.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(response);
            
            //Act
            var actual = await _controller.Get(id);
            
            //Assert
            Assert.IsType<OkObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Success on getting list")]
        [Trait("Get Game list with success", "Get")]
        public async Task GetGameList_WithValidData_MustResultOk()
        {
            //Arrange
            const int offset = 1;
            const int limit = 20;
            const int mockQuantity = 20;
            
            var id = Guid.NewGuid();
            var game = GameDtoMock.GenerateGameDto(mockQuantity);
            var response = new Response<IEnumerable<GameDto>>();
            response.SetResult(game);            

            _gameServiceMock
                .Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(response);
            
            //Act
            var actual = await _controller.Get(offset, limit);
            
            //Assert
            Assert.IsType<OkObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Fail on getting")]
        [Trait("Get nonexistent Game must result Bad Request", "Get")]
        public async Task GetGame_NonExistent_MustResultBadRequest()
        {
            //Arrange
            var id = Guid.NewGuid();
            _notificationHandler.AddNotification(new Notification(key:"NotFound", message:"Game not found with given id"));

            //Act
            var actual = await _controller.Get(id);
            
            //Assert
            Assert.IsType<NotFoundObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Success on creating")]
        [Trait("Create Game with success", "Create")]
        public async Task CreateGame_WithValidData_MustResultOk()
        {
            //Arrange
            var request = CreateGameRequestDtoMock.GenerateCreateGameRequestDto();

            var response = new Response<Guid>();
            response.SetResult(Guid.NewGuid());            

            _gameServiceMock.Setup(x => x.Create(It.IsAny<GameDto>())).ReturnsAsync(response);
            
            //Act
            var actual = await _controller.Create(request);
            
            //Assert
            Assert.IsType<OkObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Fail on creating")]
        [Trait("Create Game with empty title must result in Bad Request", "Create")]
        public async Task CreateGame_WithEmptyName_MustResultBadRequest()
        {
            //Arrange
            var request = CreateGameRequestDtoMock.GenerateCreateGameRequestDtoWithTitleEmpty();
            
            _notificationHandler.AddNotification(new Notification(key: "InputValidation", message: "Name can't be empty"));

            //Act
            var actual = await _controller.Create(request);
            
            //Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Success on updating")]
        [Trait("Update Game with success", "Update")]
        public async Task UpdateGame_WithValidData_MustResultOk()
        {
            //Arrange
            var request = GameDtoMock.GenerateGameDto();

            var response = new Response<bool>();
            response.SetResult(true);            

            _gameServiceMock.Setup(x => x.Update(It.IsAny<GameDto>())).ReturnsAsync(response);
            
            //Act
            var actual = await _controller.Update(request);
            
            //Assert
            Assert.IsType<OkObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Fail on updating")]
        [Trait("Update nonexistent Game must result in Not Found", "Update")]
        public async Task UpdateGame_NonExistent_MustResultNotFound()
        {
            //Arrange
            var request = GameDtoMock.GenerateGameDto();
            _notificationHandler.AddNotification(new Notification(key: "NotFound", message: "Game not found with given id"));

            //Act
            var actual = await _controller.Update(request);
            
            //Assert
            Assert.IsType<NotFoundObjectResult>(actual);
        }
        
        [Fact(DisplayName = "Fail on deleting")]
        [Trait("Delete nonexistent Game must result in Bad Request", "Delete")]
        public async Task DeleteGame_NonExistent_MustResultNotFound()
        {
            //Arrange
            var request = Guid.NewGuid();
            _notificationHandler.AddNotification(new Notification(key: "NotFound", message: "Game not found with given id"));

            //Act
            var actual = await _controller.Delete(request);
            
            //Assert
            Assert.IsType<NotFoundObjectResult>(actual);
        }
    }
}