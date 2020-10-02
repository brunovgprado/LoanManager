using Bogus;
using LoanManager.Domain.Entities;
using LoanManager.Tests.Utils;
using System;

namespace LoanManager.Tests.Builders
{
    public class FriendBuilder
    {
        private readonly Faker _faker;
        private readonly Friend _instance;

        public FriendBuilder()
        {
            _faker = FakerPtbr.CreateFaker();
            _instance = new Friend
            {
                Name = _faker.Random.String(),
                PhoneNumber = _faker.Random.String()
            };
        }

        public Friend WithNameEmpty()
        {
            _instance.Name = String.Empty;
            return _instance;
        }

        public Friend Build()
        {
            return _instance;
        }
    }
}
