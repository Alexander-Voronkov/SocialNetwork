using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendships.Queries.GetSingleFriendship
{
    public class GetSingleFriendshipQueryValidator : AbstractValidator<GetSingleFriendshipQuery>
    {
        public GetSingleFriendshipQueryValidator() 
        {
            RuleFor(x => x.FriendshipId)
                .NotNull()
                .WithMessage("Friendship id cannot be null");
        }
    }
}
