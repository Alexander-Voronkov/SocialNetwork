using FluentValidation;

namespace Application.Chats.Queries.GetSingleChat
{
    public class GetSingleChatQueryValidator : AbstractValidator<GetSingleChatQuery>
    {
        public GetSingleChatQueryValidator() 
        {
            RuleFor(x => x.ChatId)
                .NotNull()
                .WithMessage("Chat id cannot be null");
        }
    }
}
