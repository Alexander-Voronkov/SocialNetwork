using FluentValidation;

namespace Application.Friendships.Commands.CreateFriendship
{
    public class CreateFriendshipCommandValidator : AbstractValidator<CreateFriendshipCommand>
    {
        public CreateFriendshipCommandValidator() 
        {
            RuleFor(x => x.SecondUserId)
                .NotNull()
                .WithMessage("Future friend id cannot be null");
        }
    }
}
