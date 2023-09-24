using FluentValidation;

namespace Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.PostId)
                .NotNull()
                .WithMessage("Post id cannot be null");

            RuleFor(x => x.CommentBody)
                .NotEmpty()
                .WithMessage("Comment body cannot be empty")
                .NotNull()
                .WithMessage("Comment body cannot be null");

        }
    }
}
