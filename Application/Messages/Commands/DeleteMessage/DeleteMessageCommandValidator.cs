using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Commands.DeleteMessage
{
    public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageCommandValidator() 
        {
            RuleFor(x => x.MessageId)
                .NotNull()
                .WithMessage("Message id cannot be null");
        }
    }
}
