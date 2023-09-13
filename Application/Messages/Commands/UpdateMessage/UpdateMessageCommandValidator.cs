using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Commands.UpdateMessage
{
    public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageCommandValidator()
        {
            RuleFor(x => x.MessageId)
                    .NotNull()
                    .WithMessage("Chat id cannot be null");

            RuleFor(x => x.MessageBody)
                    .NotNull()
                    .WithMessage("Message text cannot be null")
                    .NotEmpty()
                    .WithMessage("Message text cannot be empty");
        }
    }
}
