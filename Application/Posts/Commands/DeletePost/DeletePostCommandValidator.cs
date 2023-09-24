using FluentValidation;

namespace Application.Posts.Commands.DeletePost
{
    public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator() 
        {
            RuleFor(x => x.PostId)
                .NotNull()
                .WithMessage("Post id cannot be null");
        }
    }
}
