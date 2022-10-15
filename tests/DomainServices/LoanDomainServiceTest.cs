using System;
using System.Linq;
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
using Xunit;

namespace LoanManager.Tests.DomainServices
{
    public class LoanDomainServiceTest
    {
        private readonly ILoanDomainService _loanDomainService;
        private readonly Mock<ILoanRepository> _loanRepositoryMock;
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IFriendRepository> _friendRepositoryMock;

        public LoanDomainServiceTest()
        {
            var createLoanValidator = new CreateLoanValidator();
            _gameRepositoryMock = new Mock<IGameRepository>();
            _friendRepositoryMock = new Mock<IFriendRepository>();
            _loanRepositoryMock = new Mock<ILoanRepository>();
            

            _loanDomainService = new LoanDomainService(
                createLoanValidator,
                _loanRepositoryMock.Object,
                _gameRepositoryMock.Object,
                _friendRepositoryMock.Object);
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
        
        [Fact(DisplayName = "Create loan with empty game id throws exception")]
        [Trait("Loan Domain Service", "CreateAsync")]
        public async Task CreateGame_WithGameIdEmpty_MustThrowException()
        {
            //Arrange
            var entity = LoanMock.GenerateLoanWithGameIdEmpty();

            //Act/Assert
            await Assert.ThrowsAsync<ValidationException>(async
                () => await _loanDomainService.CreateAsync(entity));
        }

        [Fact(DisplayName = "Create loan with empty friend id throws exception")]
        [Trait("Loan Domain Service", "CreateAsync")]
        public async Task CreateGame_WithFriendIdEmpty_MustThrowException()
        {
            //Arrange
            var entity = LoanMock.GenerateLoanWithFriendIdEmpty();

            //Act/Assert
            await Assert.ThrowsAsync<ValidationException>(async
                () => await _loanDomainService.CreateAsync(entity));
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

            _loanRepositoryMock.Setup(x => x.GetAsync(offset, limit))
                .ReturnsAsync(entity);

            //Act
            var result = await _loanDomainService.GetAsync(offset, limit);
            
            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Count(), limit);
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
    }
}
