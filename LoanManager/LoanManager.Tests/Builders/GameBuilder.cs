using Bogus;
using LoanManager.Domain.Entities;
using LoanManager.Tests.Utils;
using System;

namespace LoanManager.Tests.Builders
{
    public class GameBuilder
    {
        private readonly Faker _faker;
        private readonly Game _instance;

        public GameBuilder()
        {
            _faker = FakerPtbr.CreateFaker();
            _instance = new Game
            {
                Title = _faker.Random.String(),
                Description = _faker.Random.String(),
                Genre = _faker.Random.String(),
                Platform = _faker.Random.String()
            };
        }

        public Game WithTitleEmpty()
        {
            _instance.Title = String.Empty;
            return _instance;
        }

        public Game WithPlatformEmpty()
        {
            _instance.Platform = String.Empty;
            return _instance;
        }

        public Game Build()
        {
            return _instance;
        }
    }
}
