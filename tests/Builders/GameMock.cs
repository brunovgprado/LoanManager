using Bogus;
using LoanManager.Domain.Entities;
using System;
using System.Collections.Generic;

namespace LoanManager.Tests.Builders
{
    public static class GameMock
    {
        private static Faker<Game> Build()
        {
            return new Faker<Game>()
                .RuleFor(g => g.Id, x => x.Random.Guid())
                .RuleFor(g => g.Title, x => x.Commerce.ProductName())
                .RuleFor(g => g.Description, x => x.Commerce.ProductDescription())
                .RuleFor(g => g.Platform, x => GeneratePlatform(x.Random.Int(0, 1)));
        }

        public static Game GenerateGame()
        {
            return Build().Generate();
        }
        
        public static IEnumerable<Game> GenerateGame(int quantity)
        {
            return Build().Generate(quantity);
        }
        
        public static Game GenerateGameWithTitleEmpty()
        {
            var mock = Build().Generate();
            mock.Title = string.Empty;
            return mock;
        }
        
        public static Game GenerateGameWithPlatformEmpty()
        {
            var mock = Build().Generate();
            mock.Platform = string.Empty;
            return mock;
        }
        
        private static string GeneratePlatform(int random)
        {
            return random switch
            {
                0 => "Console",
                1 => "PC",
                _ => string.Empty
            };
        }
    }
}
