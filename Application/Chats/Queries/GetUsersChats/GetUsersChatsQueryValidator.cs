using FluentValidation;

namespace Application.Chats.Queries.GetUsersChats
{
    public class GetUsersChatsQueryValidator : AbstractValidator<GetUsersChatsQuery>
    {
        public GetUsersChatsQueryValidator() 
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
