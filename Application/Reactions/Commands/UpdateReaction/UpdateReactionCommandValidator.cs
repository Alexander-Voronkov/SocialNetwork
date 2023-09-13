using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Commands.UpdateReaction
{
    public class UpdateReactionCommandValidator : AbstractValidator<UpdateReactionCommand>
    {
        public UpdateReactionCommandValidator() 
        {
            RuleFor(x => x.ReactionId)
                .NotNull()
                .WithMessage("Reaction id cannot be null");
            RuleFor(x => x.Type)
                .NotNull()
                .WithMessage("Reaction type cannot be null");
        } 
    }
}
