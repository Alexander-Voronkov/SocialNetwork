using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chats.Commands.CreateChat
{
    public class CreateChatCommandValidator : AbstractValidator<CreateChatCommand>
    {
        public CreateChatCommandValidator() 
        {
            RuleFor(x => x.FirstUserId)
                .NotNull()
                .WithMessage("User id cannot be null")
                .NotEqual(x=>x.SecondUserId)
                .WithMessage("User id`s cannot be the same");

            RuleFor(x => x.SecondUserId)
                .NotNull()
                .WithMessage("User id cannot be null");
        }
    }
}
