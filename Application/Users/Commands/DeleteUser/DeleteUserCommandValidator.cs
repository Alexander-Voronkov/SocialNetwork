using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator() 
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("User id cannot be null")
                .NotEqual(0)
                .WithMessage("User id cannot be 0");
        }
    }
}
