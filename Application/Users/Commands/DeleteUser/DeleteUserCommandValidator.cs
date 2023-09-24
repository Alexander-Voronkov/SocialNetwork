using FluentValidation;

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
