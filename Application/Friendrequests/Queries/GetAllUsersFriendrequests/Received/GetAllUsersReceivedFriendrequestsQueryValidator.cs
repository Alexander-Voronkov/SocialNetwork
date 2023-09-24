using FluentValidation;

namespace Application.Friendrequests.Queries.GetAllUsersFriendrequests.Received
{
    public class GetAllUsersReceivedFriendrequestsQueryValidator : AbstractValidator<GetAllUsersSentFriendrequestsQuery>
    {
        public GetAllUsersReceivedFriendrequestsQueryValidator()
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
