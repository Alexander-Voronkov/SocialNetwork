using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidator() 
        {
            RuleFor(x => x.OwnerId)
                .NotNull()
                .WithMessage("Message owner id cannot be null");

            RuleFor(x => x.ChatId)
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
