﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Queries.GetAllUsersMessages
{
    public class GetAllUsersMessagesQueryValidator : AbstractValidator<GetAllUsersMessagesQuery>
    {
        public GetAllUsersMessagesQueryValidator() 
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("User id cannot be null");
        }  
    }
}