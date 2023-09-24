using FluentValidation;

namespace Application.Users.Queries.GetUsersByUsername
{
    public class GetUsersByUsernameQueryValidator : AbstractValidator<GetUsersByUsernameQuery>
    {
        public GetUsersByUsernameQueryValidator() 
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username cannot be empty")
                .NotNull()
                .WithMessage("Username cannot be null");
        }
    }
}
