using FluentValidation;

namespace Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Comment id cannot be null");

            RuleFor(x => x.CommentBody)
                .NotEmpty()
                .WithMessage("Comment body cannot be empty")
                .NotNull()
                .WithMessage("Comment body cannot be null");
        }
    }
}
