using Bogus;
using LoanManager.Domain.Entities;
using System;
using System.Collections.Generic;
using Bogus.Extensions.Brazil;

namespace LoanManager.Tests.Builders
{
    public static class FriendMock
    {
        private static Faker<Friend> Build()
        {
            return new Faker<Friend>("pt_BR")
                .RuleFor(f => f.Id, x => x.Random.Guid())
                .RuleFor(f => f.Name, x => x.Name.FullName())
                .RuleFor(f => f.PhoneNumber, x => x.Phone.PhoneNumber())
                .RuleFor(f => f.Cpf, x => x.Person.Cpf())
                .RuleFor(f => f.BlockListed, x => x.Random.Bool());
        }
        
        public static Friend GenerateFriend()
        {
            return Build().Generate();
        }
        
        public static Friend GenerateFriendWithEmptyName()
        {
            var mock = Build().Generate();
            mock.Name = string.Empty;
            return mock;
        }
        
        public static IEnumerable<Friend> GenerateFriend(int quantity)
        {
            return Build().Generate(quantity);
        }
    }
}
