using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Queries.GetAllUsersPosts
{
    public class GetAllUsersPostsQueryValidator : AbstractValidator<GetAllUsersPostsQuery>
    {
        public GetAllUsersPostsQueryValidator() 
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("User id cannot be null");
        }
    }
}
