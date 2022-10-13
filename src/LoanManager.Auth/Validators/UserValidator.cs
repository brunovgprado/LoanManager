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
            RuleFor(x => x.Email)
                .NotEmpty()
                .Must(x =>
                    !String.IsNullOrWhiteSpace(x))
                .WithMessage(Resources.UserEmailIsMandatory);
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .Must(x =>
                    !String.IsNullOrWhiteSpace(x))
                .WithMessage(Resources.UserPasswordIsMandatory);
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .Must(x =>
                    x.Length >= 9)
                .WithMessage(Resources.UserEmailMustHaveAtLeastFiveCharacters);
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .Must(x =>
                    x.Length >= 8 && x.Length <= 20)
                .WithMessage(Resources.UserPasswordMustHaveAtLeastEightCharacters);
        }
    }
}
