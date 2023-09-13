using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Commands.CreateReaction
{
    public class CreateReactionCommandValidator : AbstractValidator<CreateReactionCommand>
    {
        public CreateReactionCommandValidator()
        {
            RuleFor(x => x.OwnerId)
                .NotNull()
                .WithMessage("Reaction must have an owner");

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
