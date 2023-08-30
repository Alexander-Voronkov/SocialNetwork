using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Queries.GetPostReactions
{
    public class GetPostReactionsQueryValidator : AbstractValidator<GetPostReactionsQuery>
    {
        public GetPostReactionsQueryValidator()
        {
            RuleFor(x => x.PostId)
                .NotNull()
                .WithMessage("Post id cannot be null")
                .NotEqual(0)
                .WithMessage("Post id cannot be 0");
        }
    }
}
