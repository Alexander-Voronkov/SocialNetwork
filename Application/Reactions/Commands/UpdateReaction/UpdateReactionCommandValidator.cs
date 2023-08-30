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
            RuleFor(x => x.OwnerId)
                .NotNull()
                .WithMessage("Owner id cannot be null")
                .NotEqual(0)
                .WithMessage("Owner id cannot be 0");
            RuleFor(x => x.ReactionId)
                .NotNull()
                .WithMessage("Reaction id cannot be null")
                .NotEqual(0)
                .WithMessage("Reaction id cannot be 0");
            RuleFor(x => x.PostId)
                .NotNull()
                .WithMessage("Post id cannot be null")
                .NotEqual(0)
                .WithMessage("Post id cannot be 0");
            RuleFor(x => x.Type)
                .NotNull()
                .WithMessage("Reaction type cannot be null");
        } 
    }
}
