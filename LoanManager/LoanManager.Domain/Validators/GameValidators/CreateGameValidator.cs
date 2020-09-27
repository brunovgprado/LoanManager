using FluentValidation;
using LoanManager.Domain.Entities;
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
                .WithMessage("Game name can't be empty");

            RuleFor(x => x.Platform)
                .NotNull()
                .Must(x => 
                    !String.IsNullOrEmpty(x.Name) &&
                    !String.IsNullOrWhiteSpace(x.Name)) 
                .WithMessage("Platform name can't be empty");
        }
    }
}
