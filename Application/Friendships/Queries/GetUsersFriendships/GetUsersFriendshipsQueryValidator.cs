using FluentValidation;

namespace Application.Friendships.Queries.GetUsersFriendships
{
    public class GetUsersFriendshipsQueryValidator : AbstractValidator<GetUsersFriendshipsQuery>
    {
        public GetUsersFriendshipsQueryValidator() 
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
