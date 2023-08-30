using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Queries.GetSignlePost
{
    public class GetSinglePostQueryValidator : AbstractValidator<GetSinglePostQuery>
    {
        public GetSinglePostQueryValidator() 
        {
            RuleFor(x => x.PostId)
                .NotNull()
                .WithMessage("Post id cannot be null")
                .NotEqual(0)
                .WithMessage("Post id cannot be 0");
        }
    }
}
