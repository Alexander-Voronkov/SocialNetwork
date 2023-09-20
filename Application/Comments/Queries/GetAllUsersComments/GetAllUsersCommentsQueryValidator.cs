using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Queries.GetAllUsersComments
{
    public class GetAllUsersCommentsQueryValidator : AbstractValidator<GetAllUsersCommentsQuery>
    {
        public GetAllUsersCommentsQueryValidator() 
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("User id cannot be null");

            RuleFor(x => x.PageNumber)
                .NotNull()
                .WithMessage("There must be a page number");

            RuleFor(x => x.PageSize)
                .NotNull()
                .WithMessage("There must be a page size");
        }
    }
}
