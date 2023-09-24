using FluentValidation;

namespace Application.Posts.Queries.GetUsersPosts
{
    public class GetUsersPostsQueryValidator : AbstractValidator<GetUsersPostsQuery>
    {
        public GetUsersPostsQueryValidator() 
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
