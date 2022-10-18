using System;
using Bogus;
using LoanManager.Api.Models.Request;

namespace LoanManager.Tests.Builders
{
    public static class CreateLoanRequestDtoMock
    {
        private static Faker<CreateLoanRequestDto> Build()
        {
            return new Faker<CreateLoanRequestDto>("pt_BR")
                .RuleFor(l => l.FriendId, f => f.Random.Guid())
                .RuleFor(l => l.GameId, f => f.Random.Guid());
        }

        public static CreateLoanRequestDto GenerateLoanRequestDto()
        {
            return Build().Generate();
        }
        
        public static CreateLoanRequestDto GenerateLoanRequestDtoWithEmptyFriendId()
        {
            var loan = Build().Generate();
            loan.FriendId = Guid.Empty;
            return loan;
        }
    }
}