using FluentValidation;

namespace Application.Reactions.Queries.GetUsersReactions
{
    public class GetUsersReactionsQueryValidator : AbstractValidator<GetUsersReactionsQuery>
    {
        public GetUsersReactionsQueryValidator() 
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
