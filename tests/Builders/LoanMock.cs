using Bogus;
using LoanManager.Domain.Entities;
using System;
using System.Collections.Generic;

namespace LoanManager.Tests.Builders
{
    public static class LoanMock
    {

        private static Faker<Loan> Build()
        {
            return new Faker<Loan>()
                .RuleFor(l => l.Id, x => Guid.NewGuid())
                .RuleFor(l => l.FriendId, x => x.Random.Guid())
                .RuleFor(l => l.GameId, x => x.Random.Guid())
                .RuleFor(l => l.Friend, x => FriendMock.GenerateFriend())
                .RuleFor(l => l.Game, x => GameMock.GenerateGame())
                .RuleFor(l => l.LoanDate, x => x.Date.Past())
                .RuleFor(l => l.Returned, x => x.Random.Bool());
        }

        public static Loan GenerateLoan()
        {
            return Build().Generate();
        }
        
        public static Loan GenerateLoanWithGameIdEmpty()
        {
            var mock = Build().Generate();
            mock.GameId = Guid.Empty;
            return mock;
        }
        
        public static Loan GenerateLoanWithFriendIdEmpty()
        {
            var mock = Build().Generate();
            mock.FriendId = Guid.Empty;
            return mock;
        }
        
        public static IEnumerable<Loan> GenerateLoan(int quantity)
        {
            return Build().Generate(quantity);
        }
    }
}
