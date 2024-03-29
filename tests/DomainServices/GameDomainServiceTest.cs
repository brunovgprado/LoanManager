﻿using FluentAssertions;
using LoanManager.Domain.DomainServices;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Domain.Validators.GameValidators;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using LoanManager.Tests.Builders;
using Moq;
using Xunit;

namespace LoanManager.Tests.DomainServices
{
    public class GameDomainServiceTest
    {
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly INotificationHandler _notificationHandler;
        private readonly IGameDomainService _gameDomainService;

        public GameDomainServiceTest()
        {
            var createGameValidator = new CreateGameValidator();
            _gameRepositoryMock = new Mock<IGameRepository>();
            _notificationHandler = new NotificationHandler();

            _gameDomainService = new GameDomainService(
                createGameValidator,
                _notificationHandler,
                _gameRepositoryMock.Object);
        }

        [Fact(DisplayName = "Create game with success")]
        [Trait("Game Domain Service", "CreateAsync")]
        public async Task CreateGame_WithValidState_MustReturnSuccess()
        {
            //Arrange
            var entity = GameMock.GenerateGame();

            //Act
            var response = await _gameDomainService.CreateAsync(entity);

            //Assert
            response.Should().NotBeEmpty();
        }

        [Fact(DisplayName = "Create game with empty title throws exception")]
        [Trait("Game Domain Service", "CreateAsync")]
        public async Task CreateGame_WithTitleEmpty_MustThrowException()
        {
            //Arrange
            var entity = GameMock.GenerateGameWithTitleEmpty();

            //Act
            await _gameDomainService.CreateAsync(entity);

            //Assert
            Assert.True(_notificationHandler.GetInstance().HasNotifications);
            Assert.Contains(_notificationHandler.GetInstance().Notifications, n => n.Key.Equals("InputValidation"));

        }

        [Fact(DisplayName = "Create game with empty platform throws exception")]
        [Trait("Game Domain Service", "CreateAsync")]
        public async Task CreateGame_WithPlatformEmpty_MustThrowException()
        {
            //Arrange
            var entity = GameMock.GenerateGameWithPlatformEmpty();

            //Act
            await _gameDomainService.CreateAsync(entity);

            //Assert
            Assert.True(_notificationHandler.GetInstance().HasNotifications);
            Assert.Contains(_notificationHandler.GetInstance().Notifications, n => n.Key.Equals("InputValidation"));

        }
        
        [Fact(DisplayName = "Get game with success")]
        [Trait("Game Domain Service", "GetAsync")]
        public async Task GetGame_WithValidArguments_MustReturnSuccess()
        {
            //Arrange
            var entity = GameMock.GenerateGame();

            _gameRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(entity);

            //Act
            var result = await _gameDomainService.GetAsync(entity.Id);
            
            //Assert
            Assert.NotNull(result);
        }
        
        [Fact(DisplayName = "Get game list with success")]
        [Trait("Game Domain Service", "GetAsync")]
        public async Task GetGameList_WithValidArguments_MustReturnSuccess()
        {
            //Arrange
            const int offset = 1;
            const int limit = 20;
            const int mockQuantity = 20;
            
            var entity = GameMock.GenerateGame(mockQuantity);

            _gameRepositoryMock.Setup(x => x.GetAsync(offset, limit))
                .ReturnsAsync(entity);

            //Act
            var result = await _gameDomainService.GetAsync(offset, limit);
            
            //Assert
            Assert.NotNull(result);
            Assert.Equal(limit, result.Count());
        }
        
        [Fact(DisplayName = "Update game with success")]
        [Trait("Game Domain Service", "GetAsync")]
        public async Task UpdateGame_WithValidArguments_MustReturnSuccess()
        {
            //Arrange
            var entity = GameMock.GenerateGame();

            _gameRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Game>()))
                .ReturnsAsync(true);
            _gameRepositoryMock.Setup(x => x.CheckIfGameExistsById(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            //Act
            var atLeastOneEntityWasUpdated = await _gameDomainService.UpdateAsync(entity);
            
            //Assert
            Assert.True(atLeastOneEntityWasUpdated);
        }
        
        [Fact(DisplayName = "Delete game with success")]
        [Trait("Game Domain Service", "GetAsync")]
        public async Task DeleteGame_WithValidArguments_MustReturnSuccess()
        {
            //Arrange
            var entity = GameMock.GenerateGame();

            _gameRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            _gameRepositoryMock.Setup(x => x.CheckIfGameExistsById(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            //Act
            var atLeastOneEntityWasDeleted = await _gameDomainService.DeleteAsync(entity.Id);
            
            //Assert
            Assert.True(atLeastOneEntityWasDeleted);
        }
    }
}
