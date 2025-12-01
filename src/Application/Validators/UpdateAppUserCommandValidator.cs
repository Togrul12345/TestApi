using Application.Features.Mediator.Commands.AppUserCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateAppUserCommandValidator:AbstractValidator<UpdateAppUserCommand>
    {
        public UpdateAppUserCommandValidator()
        {
            RuleFor(a => a.UserName).NotEmpty().WithMessage("Username dont must be null").
               MinimumLength(3).WithMessage("Min length must be 3").
               Must(ContainUpper).WithMessage("Username must include Upper Character").
               Must(ContainLower).WithMessage("Username must include Lower Character").
               Must(ContainDigit).WithMessage("Username must Include Digit");
            RuleFor(a => a.PasswordHash).NotEmpty().WithMessage("Password dont must be null").
                MinimumLength(3).WithMessage("Min length must be 3").
                Must(ContainUpper).WithMessage("Password must include Upper Character").
                Must(ContainLower).WithMessage("Password must include Lower Character").
                Must(ContainDigit).WithMessage("Password must Include Digit").
                Matches("[^a-zA-Z0-9]").WithMessage("Password must include 1 nonalphanumeric character at least");
        }
        public bool ContainUpper(string userName)
        {
            return userName.Any(Char.IsUpper);
        }
        public bool ContainLower(string userName)
        {
            return userName.Any(Char.IsLower);
        }
        public bool ContainDigit(string userName)
        {

            return userName.Any(Char.IsDigit);
        }
    }
}
