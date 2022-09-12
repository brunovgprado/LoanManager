using FluentValidation;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Properties;

namespace LoanManager.Domain.Validators.LoanValidators
{
    public class CreateLoanValidator : AbstractValidator<Loan>
    {
        public CreateLoanValidator()
        {
            RuleFor(x => x.GameId)
                .NotNull()
                .NotEmpty()
                .WithMessage(Resources.GameIsMandatory);

            RuleFor(x => x.FriendId)
                .NotNull()
                .NotEmpty()
                .WithMessage(Resources.FriendIsMandatory);

        }
    }
}
