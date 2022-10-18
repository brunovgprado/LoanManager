using System;
using System.Threading.Tasks;
using AutoMapper;
using LoanManager.Application.AppServices;
using LoanManager.Application.Helpers;
using LoanManager.Application.Interfaces.AppServices;
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
        [Trait("Friend Domain Service", "CreateAsync")]
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
        [Trait("Friend Domain Service", "Update")]
        public async Task UpdateFriend_WithValidState_MustReturnSuccess()
        {
            var entity = FriendDtoMock.GenerateFriend();
            _friendDomainServiceMock.Setup(f => f.UpdateAsync(It.IsAny<Friend>()))
                .ReturnsAsync(true);
            
            //Act
            var result = await _appService.Update(entity);

            //Assert
            Assert.True(result.Result);
        }
    }
}