using FluentValidation;

namespace Application.Friendships.Commands.DeleteFriendship.ById
{
    public class DeleteFriendshipByIdCommandValidator : AbstractValidator<DeleteFriendshipByIdCommand>
    {
        public DeleteFriendshipByIdCommandValidator()
        {
            RuleFor(x => x.FriendshipId)
                .NotNull()
                .WithMessage("Friendship id cannot be null");
        }
    }
}
