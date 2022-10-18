using Bogus;
using System.Collections.Generic;
using LoanManager.Application.Models.DTO;

namespace LoanManager.Tests.Builders
{
    public static class GameDtoMock
    {
        private static Faker<GameDto> Build()
        {
            return new Faker<GameDto>()
                .RuleFor(g => g.Id, x => x.Random.Guid())
                .RuleFor(g => g.Title, x => x.Commerce.ProductName())
                .RuleFor(g => g.Description, x => x.Commerce.ProductDescription())
                .RuleFor(g => g.Platform, x => GeneratePlatform(x.Random.Int(0, 1)));
        }

        public static GameDto GenerateGameDto()
        {
            return Build().Generate();
        }
        
        public static IEnumerable<GameDto> GenerateGameDto(int quantity)
        {
            return Build().Generate(quantity);
        }
        
        public static GameDto GenerateGameDtoWithTitleEmpty()
        {
            var mock = Build().Generate();
            mock.Title = string.Empty;
            return mock;
        }
        
        public static GameDto GenerateGameDtoWithPlatformEmpty()
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