using System;
using System.Collections.Generic;
using Bogus;
using LoanManager.Application.Models.DTO;

namespace LoanManager.Tests.Builders
{
    public static class FriendDtoMock
    {
        private static Faker<FriendDto> Build()
        {
            return new Faker<FriendDto>("pt_BR")
                .RuleFor(f => f.Id, x => x.Random.Guid())
                .RuleFor(f => f.Name, x => x.Person.FullName)
                .RuleFor(f => f.PhoneNumber, x => x.Phone.PhoneNumber());
        }

        public static FriendDto GenerateFriend()
        {
            return Build().Generate();
        }
        
        public static IEnumerable<FriendDto> GenerateFriend(int quantity)
        {
            return Build().Generate(quantity);
        }
        
        public static FriendDto GenerateFriendWithIdEmptyId()
        {
            var friend = Build().Generate();
            friend.Id = Guid.Empty;
            return friend;
        }
        
        public static FriendDto GenerateFriendWithEmptyName()
        {
            var friend = Build().Generate();
            friend.Name = string.Empty;
            return friend;
        }
        
        public static FriendDto GenerateFriendWithEmptyPhoneNumber()
        {
            var friend = Build().Generate();
            friend.PhoneNumber = string.Empty;
            return friend;
        }
    }
}