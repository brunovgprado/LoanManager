using FluentAssertions;
using FluentValidation;
using LoanManager.Domain.DomainServices;
using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Domain.Validators.GameValidators;
using LoanManager.Infrastructure.DataAccess.Repositories;
using LoanManager.Tests.Builders;
using Moq;
using System.Threading.Tasks;
using LoanManager.Domain.Entities;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using Xunit;

namespace LoanManager.Tests.DomainServices
{
    public class GameDomainServiceTest
    {
        private readonly CreateGameValidator _createGameValidator;
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<INotificationHandler> _notificationHandler;
        
        public GameDomainServiceTest()
        {
            _createGameValidator = new CreateGameValidator();
            _gameRepositoryMock = new Mock<IGameRepository>();
            _friendRepositoryMock = new Mock<IFriendRepository>();
            _loanRepositoryMock = new Mock<ILoanRepository>();

            _unityOfWork = new UnitOfWork(
                _gameRepositoryMock.Object, 
                _friendRepositoryMock.Object, 
                _loanRepositoryMock.Object);

            _gameDomainService = new GameDomainService(
                createGameValidator,
                _notificationHandler.Object,
                _gameRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateGame_WithValidState_MustReturnSuccess()
        {
            //Arrange
            var entity = new GameBuilder().Build();

            //Act
            var response = await _gameDomainService.CreateAsync(entity);

            //Assert
            response.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateGame_WithTitleEmpty_MustThrowException()
        {
            //Arrange
            var entity = new GameBuilder().WithTitleEmpty();

            //Act/Assert
            await Assert.ThrowsAsync<ValidationException>(async 
                () => await _gameDomainService.CreateAsync(entity));

        }

        [Fact]
        public async Task CreateGame_WithPlatformEmpty_MustThrowException()
        {
            //Arrange
            var entity = new GameBuilder().WithPlatformEmpty();

            //Act/Assert
            await Assert.ThrowsAsync<ValidationException>(async
                () => await _gameDomainService.CreateAsync(entity));

        }
    }
}
