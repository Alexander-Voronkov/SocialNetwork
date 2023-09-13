using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator() 
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("Userid cannot be null");

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
