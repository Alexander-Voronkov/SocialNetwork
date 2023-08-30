using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Commands.DeleteReaction
{
    public class DeleteReactionCommandValidator : AbstractValidator<DeleteReactionCommand>
    {
        public DeleteReactionCommandValidator() 
        {
            RuleFor(x=>x.ReactionId)
                .NotNull()
                .WithMessage("Reaction id cannot be null")
                .NotEqual(0)
                .WithMessage("Reaction id cannot be 0");
        }
    }
}
