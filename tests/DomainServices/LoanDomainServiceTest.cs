using FluentValidation;
using LoanManager.Domain.DomainServices;
using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Domain.Validators.LoanValidators;
using LoanManager.Infrastructure.DataAccess.Repositories;
using LoanManager.Tests.Builders;
using Moq;
using System.Threading.Tasks;
using LoanManager.Domain.Entities;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using Xunit;

namespace LoanManager.Tests.DomainServices
{
    public class LoanDomainServiceTest
    {
        private readonly CreateLoanValidator _createLoanValidator;
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IFriendRepository> _friendRepositoryMock;
        private readonly Mock<INotificationHandler> _notificationHandler;

        public LoanDomainServiceTest()
        {
            _createLoanValidator = new CreateLoanValidator();
            _gameRepositoryMock = new Mock<IGameRepository>();
            _friendRepositoryMock = new Mock<IFriendRepository>();
            _loanRepositoryMock = new Mock<ILoanRepository>();

            _unityOfWork = new UnitOfWork(
                _gameRepositoryMock.Object,
                _friendRepositoryMock.Object,
                _loanRepositoryMock.Object);

            _loanDomainService = new LoanDomainService(
                createLoanValidator,
                _loanRepositoryMock.Object,
                _gameRepositoryMock.Object,
                _friendRepositoryMock.Object,
                _notificationHandler.Object);
        }

        [Fact]
        public async Task CreateGame_WithGameIdEmpty_MustThrowException()
        {
            //Arrange
            var entity = new LoanBuilder().WithGameIdEmpty();

            //Act/Assert
            await Assert.ThrowsAsync<ValidationException>(async
                () => await _loanDomainService.CreateAsync(entity));
        }

        [Fact]
        public async Task CreateGame_WithFriendIdEmpty_MustThrowException()
        {
            //Arrange
            var entity = new LoanBuilder().WithFriendIdEmpty();

            //Act/Assert
            await Assert.ThrowsAsync<ValidationException>(async
                () => await _loanDomainService.CreateAsync(entity));
        }
    }


}
