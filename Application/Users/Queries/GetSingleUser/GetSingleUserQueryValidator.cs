using FluentValidation;

namespace Application.Users.Queries.GetSingleUser
{
    public class GetSingleUserQueryValidator : AbstractValidator<GetSingleUserQuery>
    {
        public GetSingleUserQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("User id cannot be null");
        }
    }
}
