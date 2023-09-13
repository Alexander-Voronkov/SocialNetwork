using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetSingleUser
{
    public class GetSingleUserQueryValidator : AbstractValidator<GetSingleUserQuery>
    {
        public GetSingleUserQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("User id cannot be null");
        }
    }
}
