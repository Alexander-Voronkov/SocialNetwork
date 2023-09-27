using Application.Friendships.Queries.GetUsersFriendrequests.Received;
using FluentValidation;

namespace Application.Friendships.Queries.GetAllUsersFriendrequests.Received
{
    public class GetUsersReceivedFriendrequestsQueryValidator : AbstractValidator<GetUsersReceivedFriendrequestsQuery>
    {
        public GetUsersReceivedFriendrequestsQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("User id cannot be null");

            RuleFor(x => x.PageNumber)
                .NotNull()
                .WithMessage("There must be a page number");

            RuleFor(x => x.PageSize)
                .NotNull()
                .WithMessage("There must be a page size");
        }
    }
}
