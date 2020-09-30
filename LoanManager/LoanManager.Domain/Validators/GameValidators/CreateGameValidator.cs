using FluentValidation;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Text;

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
                .NotNull()    
                .WithMessage(Resources.PlatformNameISMandatory);
        }
    }
}
