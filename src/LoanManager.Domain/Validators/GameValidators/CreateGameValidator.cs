using FluentValidation;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Properties;
using System;

namespace LoanManager.Domain.Validators.GameValidators
{
    public class CreateGameValidator : AbstractValidator<Game>
    {
        public CreateGameValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .Must(x => !String.IsNullOrWhiteSpace(x))
                .WithMessage(Resources.GameNameIsMandatory);

            RuleFor(x => x.Platform)
                .NotEmpty()
                .Must(x => !String.IsNullOrWhiteSpace(x))
                .WithMessage(Resources.PlatformNameISMandatory);
        }
    }
}
