using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendships.Queries.GetAllUsersFriendships
{
    public class GetAllUsersFriendshipsQueryValidator : AbstractValidator<GetAllUsersFriendshipsQuery>
    {
        public GetAllUsersFriendshipsQueryValidator() 
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("User id cannot be null");
        }
    }
}
