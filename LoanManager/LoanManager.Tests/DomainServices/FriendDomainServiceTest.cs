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
using Xunit;

namespace LoanManager.Tests.DomainServices
{
    public class FriendDomainServiceTest
    {
        private readonly CreateFriendValidator _createFriendValidator;
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IFriendRepository> _friendRepositoryMock;
        private readonly Mock<ILoanRepository> _loanRepositoryMock;
        private readonly IUnitOfWork _unityOfWork;
        private readonly IFriendDomainService _friendDomainService;

        public FriendDomainServiceTest()
        {
            _createFriendValidator = new CreateFriendValidator();
            _gameRepositoryMock = new Mock<IGameRepository>();
            _friendRepositoryMock = new Mock<IFriendRepository>();
            _loanRepositoryMock = new Mock<ILoanRepository>();

            _unityOfWork = new UnitOfWork(
                _gameRepositoryMock.Object,
                _friendRepositoryMock.Object,
                _loanRepositoryMock.Object);

            _friendDomainService = new FriendDomainService(
                    _unityOfWork,
                    _createFriendValidator);
        }

        [Fact]
        public async Task CreateGame_Success()
        {
            //Arrange
            var entity = new FriendBuilder().Build();

            //Act
            var response = await _friendDomainService.CreateAsync(entity);

            //Assert
            response.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateGame_With_Title_Empty()
        {
            //Arrange
            var entity = new FriendBuilder().WithNameEmpty();

            //Act/Assert
            await Assert.ThrowsAsync<ValidationException>(async
                () => await _friendDomainService.CreateAsync(entity));

        }
    }
}
