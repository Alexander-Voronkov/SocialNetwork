using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Queries.GetSingleComment
{
    public class GetSingleCommentQueryValidator : AbstractValidator<GetSingleCommentQuery>
    {
        public GetSingleCommentQueryValidator() 
        {
            RuleFor(x => x.CommentId)
                .NotNull()
                .WithMessage("Comment id cannot be null");
        }
    }
}
