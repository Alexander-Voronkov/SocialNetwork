﻿using FluentValidation;

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
            
            RuleFor(x => x.PageNumber)
                .NotNull()
                .WithMessage("There must be a page number");

            RuleFor(x => x.PageSize)
                .NotNull()
                .WithMessage("There must be a page size");
        }
    }
}
