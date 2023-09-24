using FluentValidation;

namespace Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator() 
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Comment id cannot be null");
        }
    }
}
