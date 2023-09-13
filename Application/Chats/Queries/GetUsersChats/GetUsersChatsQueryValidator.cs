using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chats.Queries.GetUsersChats
{
    public class GetUsersChatsQueryValidator : AbstractValidator<GetUsersChatsQuery>
    {
        public GetUsersChatsQueryValidator() 
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("User id cannot be null");
        }
    }
}
