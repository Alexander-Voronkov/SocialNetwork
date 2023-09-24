using FluentValidation;

namespace Application.Reactions.Commands.CreateReaction
{
    public class CreateReactionCommandValidator : AbstractValidator<CreateReactionCommand>
    {
        public CreateReactionCommandValidator()
        {
            RuleFor(x => x.PostId)
                .NotNull()
                .WithMessage("Post id cannot be null");

            RuleFor(x => x.Type)
                .NotNull()
                .WithMessage("Reaction cannot be empty")
                .IsInEnum()
                .WithMessage("There is no such reaction type");
        } 
    }
}
