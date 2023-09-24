using FluentValidation;

namespace Application.Friendships.Commands.DeleteFriendship
{
    public class DeleteFriendshipCommandValidator : AbstractValidator<DeleteFriendshipCommand>
    {
        public DeleteFriendshipCommandValidator()
        {
            RuleFor(x => x.FriendshipId)
                .NotNull()
                .WithMessage("Friendship id cannot be null");
        }   
    }
}
