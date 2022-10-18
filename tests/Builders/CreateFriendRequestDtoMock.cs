using System;
using System.Collections.Generic;
using Bogus;
using LoanManager.Api.Models.Request;

namespace LoanManager.Tests.Builders
{
    public static class CreateFriendRequestDtoMock
    {
        private static Faker<CreateFriendRequestDto> Build()
        {
            return new Faker<CreateFriendRequestDto>("pt_BR")
                .RuleFor(c => c.Name, f => f.Person.FullName)
                .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber());
        }

        public static CreateFriendRequestDto GenerateFriend()
        {
            return Build().Generate();
        } 
        
        public static IEnumerable<CreateFriendRequestDto> GenerateFriend(int quantity)
        {
            return Build().Generate(quantity);
        }
        
        public static CreateFriendRequestDto GenerateFriendWithNameEmpty()
        {
            var friend = Build().Generate();
            friend.Name = string.Empty;
            return friend;
        } 
        
        public static CreateFriendRequestDto GenerateFriendWithPhoneNumberEmpty()
        {
            var friend = Build().Generate();
            friend.PhoneNumber = string.Empty;
            return friend;
        } 
    }
}