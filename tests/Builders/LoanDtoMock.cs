using Bogus;
using System;
using System.Collections.Generic;
using LoanManager.Application.Models.DTO;
using LoanManager.Tests.Builders;

namespace LoanManager.Tests.Builders
{
    public static class LoanDtoMock
    {

        private static Faker<LoanDto> Build()
        {
            return new Faker<LoanDto>()
                .RuleFor(l => l.Id, x => Guid.NewGuid())
                .RuleFor(l => l.FriendId, x => x.Random.Guid())
                .RuleFor(l => l.GameId, x => x.Random.Guid())
                .RuleFor(l => l.Friend, x => FriendDtoMock.GenerateFriend())
                .RuleFor(l => l.Game, x => GameMock.GenerateGame())
                .RuleFor(l => l.LoanDate, x => x.Date.Past())
                .RuleFor(l => l.Returned, x => x.Random.Bool());
        }

        public static LoanDto GenerateLoanDto()
        {
            return Build().Generate();
        }
        
        public static LoanDto GenerateLoanDtoWithGameIdEmpty()
        {
            var mock = Build().Generate();
            mock.GameId = Guid.Empty;
            return mock;
        }
        
        public static LoanDto GenerateLoanDtoWithFriendIdEmpty()
        {
            var mock = Build().Generate();
            mock.FriendId = Guid.Empty;
            return mock;
        }
        
        public static IEnumerable<LoanDto> GenerateLoanDto(int quantity)
        {
            return Build().Generate(quantity);
        }
    }
}