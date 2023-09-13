using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Queries.GetChatsMessages
{
    public class GetChatsMessagesQueryValidator : AbstractValidator<GetChatsMessagesQuery>
    {
        public GetChatsMessagesQueryValidator()
        {
            RuleFor(x => x.ChatId)
                .NotNull()
                .WithMessage("Chat id cannot be null");
        }
    }
}
