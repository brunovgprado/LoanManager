using FluentValidation;
using LoanManager.Domain.Entities;
using System;

namespace LoanManager.Domain.Validators.LoanValidators
{
    public class CreateLoanValidator : AbstractValidator<Loan>
    {
        public CreateLoanValidator()
        {
            RuleFor(x => x.GameId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Game id can't be empty");

            RuleFor(x => x.FriendId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Friend id can't be empty");

        }
    }
}
