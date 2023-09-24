using FluentValidation;

namespace Application.Messages.Queries.GetAllUsersMessages
{
    public class GetAllUsersMessagesQueryValidator : AbstractValidator<GetAllUsersMessagesQuery>
    {
        public GetAllUsersMessagesQueryValidator() 
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
