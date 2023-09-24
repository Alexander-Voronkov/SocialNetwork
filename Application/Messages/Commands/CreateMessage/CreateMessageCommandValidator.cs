using FluentValidation;

namespace Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidator() 
        {
            RuleFor(x => x.ChatId)
                .NotNull()
                .WithMessage("Chat id cannot be null");

            RuleFor(x => x.MessageBody)
                .NotNull()
                .WithMessage("Message text cannot be null")
                .NotEmpty()
                .WithMessage("Message text cannot be empty");
        }  
    }
}
