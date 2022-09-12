using Bogus;
using LoanManager.Domain.Entities;
using LoanManager.Tests.Utils;
using System;

namespace LoanManager.Tests.Builders
{
    public class LoanBuilder
    {
        private readonly Faker _faker;
        private readonly Loan _instance;

        public LoanBuilder()
        {
            _faker = FakerPtbr.CreateFaker();
            _instance = new Loan
            {
                GameId = _faker.Random.Guid(),
                FriendId = _faker.Random.Guid()
            };
        }

        public Loan WithGameIdEmpty()
        {
            _instance.GameId = Guid.Empty;
            return _instance;
        }

        public Loan WithFriendIdEmpty()
        {
            _instance.FriendId = Guid.Empty;
            return _instance;
        }

        public Loan Build()
        {
            return _instance;
        }
    }
}
