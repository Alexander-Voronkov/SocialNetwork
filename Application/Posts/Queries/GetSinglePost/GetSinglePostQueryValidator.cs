using FluentValidation;

namespace Application.Posts.Queries.GetSinglePost
{
    public class GetSinglePostQueryValidator : AbstractValidator<GetSinglePostQuery>
    {
        public GetSinglePostQueryValidator() 
        {
            RuleFor(x => x.PostId)
                .NotNull()
                .WithMessage("Post id cannot be null");
        }
    }
}
