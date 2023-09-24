using FluentValidation;

namespace Application.Comments.Queries.GetUsersComments
{
    public class GetUsersCommentsQueryValidator : AbstractValidator<GetUsersCommentsQuery>
    {
        public GetUsersCommentsQueryValidator() 
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
