using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty")
                .NotNull()
                .WithMessage("Email cannot be null");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number cannot be empty")
                .NotNull()
                .WithMessage("Phone number cannot be null");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("User name cannot be empty")
                .NotNull()
                .WithMessage("User name cannot be null");
        }
    }
}
