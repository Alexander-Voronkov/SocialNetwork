using FluentValidation;

namespace Application.Friendships.Queries.GetSingleFriendship
{
    public class GetSingleFriendshipQueryValidator : AbstractValidator<GetSingleFriendshipQuery>
    {
        public GetSingleFriendshipQueryValidator() 
        {
            RuleFor(x => x.FriendshipId)
                .NotNull()
                .WithMessage("Friendship id cannot be null");
        }
    }
}
