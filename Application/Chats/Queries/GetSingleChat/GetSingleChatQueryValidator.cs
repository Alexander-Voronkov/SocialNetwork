using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chats.Queries.GetSingleChat
{
    public class GetSingleChatQueryValidator : AbstractValidator<GetSingleChatQuery>
    {
        public GetSingleChatQueryValidator() 
        {
            RuleFor(x => x.ChatId)
                .NotNull()
                .WithMessage("Chat id cannot be null");
        }
    }
}
