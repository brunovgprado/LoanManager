using System;
using System.Linq;
using FluentAssertions;
using FluentValidation;
using LoanManager.Domain.DomainServices;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Interfaces.Repositories;
using LoanManager.Domain.Validators.FriendValidators;
using LoanManager.Tests.Builders;
using Moq;
using System.Threading.Tasks;
using LoanManager.Domain.Entities;
using Xunit;

namespace LoanManager.Tests.DomainServices
{
    public class FriendDomainServiceTest
    {
        private readonly IFriendDomainService _friendDomainService;
        private readonly Mock<IFriendRepository> _friendRepositoryMock;

        public FriendDomainServiceTest()
        {
            var createFriendValidator = new CreateFriendValidator();
            _friendRepositoryMock = new Mock<IFriendRepository>();

            _friendDomainService = new FriendDomainService(
                createFriendValidator,
                _friendRepositoryMock.Object);
            
            _friendRepositoryMock.Setup(x => x.CheckIfFriendExistsById(It.IsAny<Guid>()))
                .ReturnsAsync(true);
        }

        [Fact(DisplayName = "Create friend with success")]
        [Trait("Friend Domain Service", "CreateAsync")]
        public async Task CreateFriend_WithValidState_MustReturnSuccess()
        {
            //Arrange
            var entity = FriendMock.GenerateFriend();

            //Act
            var result = await _friendDomainService.CreateAsync(entity);

            //Assert
            result.Should().NotBeEmpty();
        }

        [Fact(DisplayName = "Create friend with with empty name throws exception")]
        [Trait("Friend Domain Service", "CreateAsync")]
        public async Task CreateFriend_WithNameEmpty_MustThrowException()
        {
            //Arrange
            var entity = FriendMock.GenerateFriendWithEmptyName();

            //Act/Assert
            await Assert.ThrowsAsync<ValidationException>(async
                () => await _friendDomainService.CreateAsync(entity));

        }
        
        [Fact(DisplayName = "Get friend with success")]
        [Trait("Friend Domain Service", "GetAsync")]
        public async Task GetFriend_WithValidArguments_MustReturnSuccess()
        {
            //Arrange
            var entity = FriendMock.GenerateFriend();

            _friendRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(entity);

            //Act
            var result = await _friendDomainService.GetAsync(entity.Id);
            
            //Assert
            Assert.NotNull(result);
        }
        
        [Fact(DisplayName = "Get a friends list with success")]
        [Trait("Friend Domain Service", "GetAsync")]
        public async Task GetFriends_WithValidArguments_MustReturnSuccess()
        {
            
            //Arrange
            const int offset = 1;
            const int limit = 20;
            const int mockQuantity = 20;
            
            var entity = FriendMock.GenerateFriend(mockQuantity);

            _friendRepositoryMock.Setup(x => x.GetAsync(offset, limit))
                .ReturnsAsync(entity);

            //Act
            var result = await _friendDomainService.GetAsync(offset, limit);
            
            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Count(), limit);
        }
        
        [Fact(DisplayName = "Update friend with success")]
        [Trait("Friend Domain Service", "GetAsync")]
        public async Task UpdateFriend_WithValidArguments_MustReturnSuccess()
        {
            //Arrange
            var entity = FriendMock.GenerateFriend();

            _friendRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Friend>()))
                .ReturnsAsync(true);
            _friendRepositoryMock.Setup(x => x.CheckIfFriendExistsById(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            //Act
            var atLeastOneEntityWasUpdated = await _friendDomainService.UpdateAsync(entity);
            
            //Assert
            Assert.True(atLeastOneEntityWasUpdated);
        }
        
        
        [Fact(DisplayName = "Delete friend with success")]
        [Trait("Friend Domain Service", "GetAsync")]
        public async Task DeleteFriend_WithValidArguments_MustReturnSuccess()
        {
            //Arrange
            var entity = FriendMock.GenerateFriend();

            _friendRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            _friendRepositoryMock.Setup(x => x.CheckIfFriendExistsById(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            //Act
            var atLeastOneEntityWasDeleted = await _friendDomainService.DeleteAsync(entity.Id);
            
            //Assert
            Assert.True(atLeastOneEntityWasDeleted);
        }
    }
}
