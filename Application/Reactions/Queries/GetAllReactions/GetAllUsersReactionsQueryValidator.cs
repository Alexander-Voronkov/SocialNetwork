using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Queries.GetAllReactions
{
    public class GetAllUsersReactionsQueryValidator : AbstractValidator<GetAllUsersReactionsQuery>
    {
        public GetAllUsersReactionsQueryValidator() 
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("User id cannot be null");
        }
    }
}
