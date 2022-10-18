using Bogus;
using System.Collections.Generic;
using LoanManager.Api.Models.Request;

namespace LoanManager.Tests.Builders
{
    public static class CreateGameRequestDtoMock
    {
        private static Faker<CreateGameRequestDto> Build()
        {
            return new Faker<CreateGameRequestDto>()
                .RuleFor(g => g.Title, x => x.Commerce.ProductName())
                .RuleFor(g => g.Description, x => x.Commerce.ProductDescription())
                .RuleFor(g => g.Platform, x => GeneratePlatform(x.Random.Int(0, 1)));
        }

        public static CreateGameRequestDto GenerateCreateGameRequestDto()
        {
            return Build().Generate();
        }
        
        public static IEnumerable<CreateGameRequestDto> GenerateCreateGameRequestDto(int quantity)
        {
            return Build().Generate(quantity);
        }
        
        public static CreateGameRequestDto GenerateCreateGameRequestDtoWithTitleEmpty()
        {
            var mock = Build().Generate();
            mock.Title = string.Empty;
            return mock;
        }
        
        public static CreateGameRequestDto GenerateCreateGameRequestDtoWithPlatformEmpty()
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