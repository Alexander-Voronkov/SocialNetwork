using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendrequests.Queries.GetAllUsersFriendrequests
{
    public class GetAllUsersFriendrequestsQueryValidator : AbstractValidator<GetAllUsersFriendrequestsQuery>
    {
        public GetAllUsersFriendrequestsQueryValidator() 
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("User id cannot be null");
        }
    }
}
