using FluentValidation;
using LoanManager.Auth.Models;
using LoanManager.Auth.Properties;
using System;

namespace LoanManager.Auth.Validators
{
    public class UserValidator : AbstractValidator<UserCredentials>
    {
        public UserValidator()
        {
            // Email can't be empty
            RuleFor(x => x.Email)
                .NotEmpty()
                .Must(x =>
                    !String.IsNullOrWhiteSpace(x))
                .WithMessage(Resources.UserEmailIsMandatory);

            // Email can't be empty
            RuleFor(x => x.Password)
                .NotEmpty()
                .Must(x =>
                    !String.IsNullOrWhiteSpace(x))
                .WithMessage(Resources.UserPasswordIsMandatory);

            // The full email adress must be at least 9 characters long
            RuleFor(x => x.Email)
                .NotEmpty()
                .Must(x =>
                    x.Length >= 9)
                .WithMessage(Resources.UserEmailMustHaveAtLeastFiveCharacters);

            // The password must be at least 8 characters long
            RuleFor(x => x.Password)
                .NotEmpty()
                .Must(x =>
                    x.Length >= 8)
                .WithMessage(Resources.UserPassrdMustHaveAtLeastEightCharacters);
        }
    }
}
