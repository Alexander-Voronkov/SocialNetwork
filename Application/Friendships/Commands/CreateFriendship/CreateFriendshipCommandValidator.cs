using FluentValidation;

namespace Application.Friendships.Commands.CreateFriendship
{
    public class CreateFriendshipCommandValidator : AbstractValidator<CreateFriendshipCommand>
    {
        public CreateFriendshipCommandValidator() 
        {
            RuleFor(x => x.FirstUserId)
                .NotNull()
                .WithMessage("User id cannot be null");
            RuleFor(x => x.SecondUserId)
                .NotNull()
                .WithMessage("User id cannot be null");
        }
    }
}
