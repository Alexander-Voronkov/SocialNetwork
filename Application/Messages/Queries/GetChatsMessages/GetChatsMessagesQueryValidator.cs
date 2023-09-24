using FluentValidation;

namespace Application.Messages.Queries.GetChatsMessages
{
    public class GetChatsMessagesQueryValidator : AbstractValidator<GetChatsMessagesQuery>
    {
        public GetChatsMessagesQueryValidator()
        {
            RuleFor(x => x.ChatId)
                .NotNull()
                .WithMessage("Chat id cannot be null");

            RuleFor(x => x.PageNumber)
                .NotNull()
                .WithMessage("There must be a page number");

            RuleFor(x => x.PageSize)
                .NotNull()
                .WithMessage("There must be a page size");
        }
    }
}
