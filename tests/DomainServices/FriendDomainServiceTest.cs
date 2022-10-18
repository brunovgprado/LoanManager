using FluentAssertions;
using FluentValidation;
using LoanManager.Domain.DomainServices;
using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Domain.Validators.FriendValidators;
using LoanManager.Infrastructure.DataAccess.Repositories;
using LoanManager.Tests.Builders;
using Moq;
using System.Threading.Tasks;
using LoanManager.Domain.Entities;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using Xunit;

namespace LoanManager.Tests.DomainServices
{
    public class FriendDomainServiceTest
    {
        private readonly CreateFriendValidator _createFriendValidator;
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IFriendRepository> _friendRepositoryMock;
        private readonly Mock<INotificationHandler> _notificationHandler;

        public FriendDomainServiceTest()
        {
            _createFriendValidator = new CreateFriendValidator();
            _gameRepositoryMock = new Mock<IGameRepository>();
            _friendRepositoryMock = new Mock<IFriendRepository>();
            _loanRepositoryMock = new Mock<ILoanRepository>();

            _friendDomainService = new FriendDomainService(
                createFriendValidator,
                _notificationHandler.Object,
                _friendRepositoryMock.Object);
            
            _friendRepositoryMock.Setup(x => x.CheckIfFriendExistsById(It.IsAny<Guid>()))
                .ReturnsAsync(true);
        }

        [Fact]
        public async Task CreateFriend_WithValidState_MustReturnSuccess()
        {
            //Arrange
            var entity = new FriendBuilder().Build();

            //Act
            var response = await _friendDomainService.CreateAsync(entity);

            //Assert
            response.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateFriend_WithNameEmpty_MustThrowException()
        {
            //Arrange
            var entity = new FriendBuilder().WithNameEmpty();

            //Act/Assert
            await Assert.ThrowsAsync<ValidationException>(async
                () => await _friendDomainService.CreateAsync(entity));

        }
    }
}
