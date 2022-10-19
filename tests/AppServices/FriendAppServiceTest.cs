using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LoanManager.Application.AppServices;
using LoanManager.Application.Helpers;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Tests.Builders;
using Moq;
using Xunit;

namespace LoanManager.Tests.AppServices
{
    public class FriendAppServiceTest
    {
        private readonly Mock<IFriendDomainService> _friendDomainServiceMock;
        private readonly IFriendAppService _appService;
        private readonly IMapper _mapperMock;

        public FriendAppServiceTest()
        {
            _friendDomainServiceMock = new Mock<IFriendDomainService>();
            if (_mapperMock is null)
            {
                var config = new MapperConfiguration(c =>
                {
                    c.AddProfile(new AppAutoMapperProfile());
                });
                var mapper = config.CreateMapper();
                _mapperMock = mapper;
            }

            _appService = new FriendAppService(_friendDomainServiceMock.Object, _mapperMock);
        }

        [Fact(DisplayName = "Create friend with success")]
        [Trait("Create Friend with success", "CreateAsync")]
        public async Task CreateFriend_WithValidState_MustReturnSuccess()
        {
            //Arrange
            var id = Guid.NewGuid();
            var entity = FriendDtoMock.GenerateFriend();
            _friendDomainServiceMock.Setup(f => f.CreateAsync(It.IsAny<Friend>()))
                .ReturnsAsync(id);
            //Act
            var result = await _appService.CreateAsync(entity);

            //Assert
            Assert.False(Guid.Empty.Equals(result.Result));
        }
        
        [Fact(DisplayName = "Update friend with success")]
        [Trait("Update Friend with success", "Update")]
        public async Task UpdateFriend_WithValidState_MustReturnSuccess()
        {
            //Arrange
            var entity = FriendDtoMock.GenerateFriend();
            _friendDomainServiceMock.Setup(f => f.UpdateAsync(It.IsAny<Friend>()))
                .ReturnsAsync(true);
            
            //Act
            var result = await _appService.Update(entity);

            //Assert
            Assert.True(result.Result);
        }

        [Fact(DisplayName = "Get friend with success")]
        [Trait("Get Friend with success", "Get")]
        public async Task GetFriend_WithValidData_MustReturnSuccess()
        {
            //Arrange
            var result = FriendMock.GenerateFriend();
            _friendDomainServiceMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(result);
            var id = Guid.NewGuid();
            
            //Act
            var actual = await _appService.Get(id);

            //Assert
            Assert.IsType<FriendDto>(actual.Result);
        } 
        
        [Fact(DisplayName = "Get friend list with success")]
        [Trait("Get Friend list with success", "Get")]
        public async Task GetFriendList_WithValidData_MustReturnSuccess()
        {
            //Arrange
            const int offset = 1;
            const int limit = 20;
            const int mockQuantity = 20;
            
            var result = FriendMock.GenerateFriend(mockQuantity);
            _friendDomainServiceMock
                .Setup(x => x.GetAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(result);
            var id = Guid.NewGuid();
            
            //Act
            var actual = await _appService.Get(offset, limit);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<FriendDto>>(actual.Result);
        } 
    }
}