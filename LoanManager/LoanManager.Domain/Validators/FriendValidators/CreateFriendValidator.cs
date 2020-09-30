using FluentValidation;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Domain.Validators.FriendValidators
{
    public class CreateFriendValidator : AbstractValidator<Friend>
    {
        public CreateFriendValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Must(x => 
                    !String.IsNullOrWhiteSpace(x))
                .WithMessage(Resources.FriendNameIsMandatory);
        }
    }
}
