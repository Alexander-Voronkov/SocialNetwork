using FluentValidation;

namespace Application.Friendships.Commands.DeleteFriendship.ByUserId
{
    public class DeleteFriendshipByUserIdCommandValidator : AbstractValidator<DeleteFriendshipByUserIdCommand>
    {
        public DeleteFriendshipByUserIdCommandValidator() 
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("User id cannot be null");
        }
    }
}
