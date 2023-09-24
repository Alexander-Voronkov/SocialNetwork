using FluentValidation;

namespace Application.Comments.Queries.GetSingleComment
{
    public class GetSingleCommentQueryValidator : AbstractValidator<GetSingleCommentQuery>
    {
        public GetSingleCommentQueryValidator() 
        {
            RuleFor(x => x.CommentId)
                .NotNull()
                .WithMessage("Comment id cannot be null");
        }
    }
}
