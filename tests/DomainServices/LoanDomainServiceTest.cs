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
using Xunit;

namespace LoanManager.Tests.DomainServices
{
    public class LoanDomainServiceTest
    {
        private readonly CreateLoanValidator _createLoanValidator;
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IFriendRepository> _friendRepositoryMock;
        private readonly Mock<ILoanRepository> _loanRepositoryMock;
        private readonly IUnitOfWork _unityOfWork;
        private readonly ILoanDomainService _loanDomainService;

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
                    _unityOfWork,
                    _createLoanValidator);
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
