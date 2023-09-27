using FluentValidation;

namespace Application.Friendships.Commands.AcceptFriendship
{
    public class AcceptFriendshipCommandValidator : AbstractValidator<AcceptFriendshipCommand>
    {
        public AcceptFriendshipCommandValidator() 
        {
            RuleFor(x => x.FriendshipId)
                .NotNull()
                .WithMessage("Friendship id cannot be null.");
        }
    }
}
