using FluentValidation;

namespace Application.Comments.Queries.GetPostComments
{
    public class GetPostCommentsQueryValidator : AbstractValidator<GetPostCommentsQuery>
    {
        public GetPostCommentsQueryValidator() 
        {
            RuleFor(x => x.PostId)
                .NotNull()
                .WithMessage("Post id cannot be null");

            RuleFor(x => x.PageNumber)
                .NotNull()
                .WithMessage("There must be a page number");

            RuleFor(x => x.PageSize)
                .NotNull()
                .WithMessage("There must be a page size");
        }
    }
}
