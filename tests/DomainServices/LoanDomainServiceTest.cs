using System;
using System.Linq;
using FluentValidation;
using LoanManager.Domain.DomainServices;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Domain.Validators.LoanValidators;
using LoanManager.Domain.Exceptions;
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
        private readonly ILoanDomainService _loanDomainService;
        private readonly Mock<ILoanRepository> _loanRepositoryMock;
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IFriendRepository> _friendRepositoryMock;
        private readonly INotificationHandler _notificationHandler;

        public LoanDomainServiceTest()
        {
            var createLoanValidator = new CreateLoanValidator();
            _gameRepositoryMock = new Mock<IGameRepository>();
            _friendRepositoryMock = new Mock<IFriendRepository>();
            _loanRepositoryMock = new Mock<ILoanRepository>();
            _notificationHandler = new NotificationHandler();

            _loanDomainService = new LoanDomainService(
                createLoanValidator,
                _loanRepositoryMock.Object,
                _gameRepositoryMock.Object,                
                _friendRepositoryMock.Object,
                _notificationHandler);
        }

        [Fact(DisplayName = "Create loan with success")]
        [Trait("Loan Domain Service", "CreateAsync")]
        public async Task CreateGame_WithValidData_ReturnSuccess()
        {
            //Arrange
            var entity = LoanMock.GenerateLoan();
            _friendRepositoryMock.Setup(x => x.CheckIfFriendExistsById(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            _gameRepositoryMock.Setup(x => x.CheckIfGameExistsById(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            
            //Act
            var createdEntityId = await _loanDomainService.CreateAsync(entity);
            
            //Assert
            Assert.Equal(entity.Id ,createdEntityId);
        }
        
        [Fact(DisplayName = "Create loan with game not available throws exception")]
        [Trait("Loan Domain Service", "CreateAsync")]
        public async Task CreateLoan_GameNotAvailable_MustThrowException()
        {
            //Arrange
            _gameRepositoryMock.Setup(x => x.CheckIfGameExistsById(It.IsAny<Guid>())).ReturnsAsync(true);
            _friendRepositoryMock.Setup(x => x.CheckIfFriendExistsById(It.IsAny<Guid>())).ReturnsAsync(true);
            _loanRepositoryMock.Setup(x => x.CheckIfGameIsOnLoan(It.IsAny<Guid>())).ReturnsAsync(true);
            
            var entity = LoanMock.GenerateLoan();

            //Act
            await _loanDomainService.CreateAsync(entity);

            //Assert
            Assert.True(_notificationHandler.GetInstance().HasNotifications);
            Assert.Contains(_notificationHandler.GetInstance().Notifications, n => n.Key.Equals("BusinessRule"));
        }
        
        [Fact(DisplayName = "Create loan with nonexistent game throws exception")]
        [Trait("Loan Domain Service", "CreateAsync")]
        public async Task CreateLoan_GameOrFriendNotExists_MustThrowException()
        {
            //Arrange
            var entity = LoanMock.GenerateLoan();

            //Act
            await _loanDomainService.CreateAsync(entity);

            //Assert
            Assert.True(_notificationHandler.GetInstance().HasNotifications);
            Assert.Contains(_notificationHandler.GetInstance().Notifications, n => n.Key.Equals("NotFound"));
        }

        [Fact(DisplayName = "Create loan with empty friend id throws exception")]
        [Trait("Loan Domain Service", "CreateAsync")]
        public async Task CreateLoan_WithFriendIdEmpty_MustThrowException()
        {
            //Arrange
            var entity = LoanMock.GenerateLoanWithFriendIdEmpty();

            //Act
            await _loanDomainService.CreateAsync(entity);

            //Assert
            Assert.True(_notificationHandler.GetInstance().HasNotifications);
            Assert.Contains(_notificationHandler.GetInstance().Notifications, n => n.Key.Equals("InputValidation"));
        }
        
        [Fact(DisplayName = "Create loan with empty game id throws exception")]
        [Trait("Loan Domain Service", "CreateAsync")]
        public async Task CreateLoan_WithGameIdEmpty_MustThrowException()
        {
            //Arrange
            var entity = LoanMock.GenerateLoanWithGameIdEmpty();

            //Act
            await _loanDomainService.CreateAsync(entity);

            //Assert
            Assert.True(_notificationHandler.GetInstance().HasNotifications);
            Assert.Contains(_notificationHandler.GetInstance().Notifications, n => n.Key.Equals("InputValidation"));
        }
        
        [Fact(DisplayName = "Get loan with success")]
        [Trait("Loan Domain Service", "GetAsync")]
        public async Task GetLoan_WithValidArguments_MustReturnSuccess()
        {
            //Arrange
            var entity = LoanMock.GenerateLoan();

            _loanRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(entity);

            //Act
            var result = await _loanDomainService.GetAsync(entity.Id);
            
            //Assert
            Assert.NotNull(result);
        }
        
        [Fact(DisplayName = "Get loan list with success")]
        [Trait("Loan Domain Service", "GetAsync")]
        public async Task GetLoanList_WithValidArguments_MustReturnSuccess()
        {
            //Arrange
            const int offset = 1;
            const int limit = 20;
            const int mockQuantity = 20;
            
            var entity = LoanMock.GenerateLoan(mockQuantity);

            _loanRepositoryMock.Setup(x => x.GetAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(entity);

            //Act
            var result = await _loanDomainService.GetAsync(offset, limit);
            
            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count() <= limit);
        }
        
        [Fact(DisplayName = "Get loan history by game with success")]
        [Trait("Loan Domain Service", "GetAsync")]
        public async Task GetLoanHistory_WithValidArguments_MustReturnSuccess()
        {
            //Arrange
            var loanId = Guid.NewGuid();
            const int offset = 1;
            const int limit = 20;
            const int mockQuantity = 20;
            
            var entity = LoanMock.GenerateLoan(mockQuantity);

            _loanRepositoryMock.Setup(x => x.ReadLoanHistoryByGameAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(entity);

            //Act
            var result = await _loanDomainService.ReadLoanHistoryByGameAsync(loanId, offset, limit);
            
            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count() <= limit);
        }
        
        [Fact(DisplayName = "Update loan with success")]
        [Trait("Loan Domain Service", "UpdateAsync")]
        public async Task UpdateLoan_WithValidArguments_MustReturnSuccess()
        {
            //Arrange
            var entity = LoanMock.GenerateLoan();

            _loanRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Loan>()))
                .ReturnsAsync(true);
            _loanRepositoryMock.Setup(x => x.CheckIfyIfLoanExistsById(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            //Act
            var atLeastOneEntityWasUpdated = await _loanDomainService.UpdateAsync(entity);
            
            //Assert
            Assert.True(atLeastOneEntityWasUpdated);
        }
        
        [Fact(DisplayName = "Delete loan with success")]
        [Trait("Loan Domain Service", "DeleteAsync")]
        public async Task DeleteLoan_WithValidArguments_MustReturnSuccess()
        {
            //Arrange
            var entity = LoanMock.GenerateLoan();

            _loanRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            _loanRepositoryMock.Setup(x => x.CheckIfyIfLoanExistsById(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            //Act
            var atLeastOneEntityWasUpdated = await _loanDomainService.DeleteAsync(entity.Id);
            
            //Assert
            Assert.True(atLeastOneEntityWasUpdated);
        }
        
        [Fact(DisplayName = "Finish loan with success")]
        [Trait("Loan Domain Service", "DeleteAsync")]
        public async Task FinishLoan_WithValidArguments_MustReturnSuccess()
        {
            //Arrange
            var entity = LoanMock.GenerateLoan();

            _loanRepositoryMock.Setup(x => x.FinishLoanAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            _loanRepositoryMock.Setup(x => x.CheckIfyIfLoanExistsById(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            //Act
            var atLeastOneEntityWasUpdated = await _loanDomainService.FinishLoanAsync(entity.Id);
            
            //Assert
            Assert.True(atLeastOneEntityWasUpdated);
        }
    }
}
