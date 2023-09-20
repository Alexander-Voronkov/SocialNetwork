using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Queries.GetAllPostComments
{
    public class GetAllPostCommentsQueryValidator : AbstractValidator<GetAllPostCommentsQuery>
    {
        public GetAllPostCommentsQueryValidator() 
        {
            RuleFor(x => x.PostId)
                .NotNull()
                .WithMessage("Post id cannot be null");

            RuleFor(x => x.PageNumber)
                .NotNull()
                .WithMessage("There must be a page number");

            RuleFor(x => x.PageSize)
                .NotNull()
                .WithMessage("There must be a page size");
        }
    }
}
