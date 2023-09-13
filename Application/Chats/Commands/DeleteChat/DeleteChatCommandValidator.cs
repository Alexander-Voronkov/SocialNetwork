using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chats.Commands.DeleteChat
{
    public class DeleteChatCommandValidator : AbstractValidator<DeleteChatCommand>
    {
        public DeleteChatCommandValidator() 
        {
            RuleFor(x => x.ChatId)
                .NotNull()
                .WithMessage("Chat id cannot be empty");
        } 
    }
}
