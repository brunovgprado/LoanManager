using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using LoanManager.Domain.DomainServices;
using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Domain.Validators.GameValidators;
using LoanManager.Infrastructure.DataAccess.Repositories;
using LoanManager.Tests.Builders;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LoanManager.Tests.DomainServices
{
    public class GameDomainServiceTest
    {
        private readonly CreateGameValidator _createGameValidator;
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IFriendRepository> _friendRepositoryMock;
        private readonly Mock<ILoanRepository> _loanRepositoryMock;
        private readonly IUnitOfWork _unityOfWork;
        private readonly IGameDomainService _gameDomainService;

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
                    _unityOfWork,
                    _createGameValidator);
        }

        [Fact]
        public async Task CreateGame_Success()
        {
            //Arrange
            var entity = new GameBuilder().Build();

            //Act
            var response = await _gameDomainService.CreateAsync(entity);

            //Assert
            response.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateGame_With_Title_Empty()
        {
            //Arrange
            var entity = new GameBuilder().WithTitleEmpty();

            //Act/Assert
            await Assert.ThrowsAsync<ValidationException>(async 
                () => await _gameDomainService.CreateAsync(entity));

        }

        [Fact]
        public async Task CreateGame_With_Platform_Empty()
        {
            //Arrange
            var entity = new GameBuilder().WithPlatformEmpty();

            //Act/Assert
            await Assert.ThrowsAsync<ValidationException>(async
                () => await _gameDomainService.CreateAsync(entity));

        }
    }
}
