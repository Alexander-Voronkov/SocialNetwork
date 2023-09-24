using FluentValidation;

namespace Application.Reactions.Commands.DeleteReaction
{
    public class DeleteReactionCommandValidator : AbstractValidator<DeleteReactionCommand>
    {
        public DeleteReactionCommandValidator() 
        {
            RuleFor(x => x.ReactionId)
                .NotNull()
                .WithMessage("Reaction id cannot be null");
        }
    }
}
