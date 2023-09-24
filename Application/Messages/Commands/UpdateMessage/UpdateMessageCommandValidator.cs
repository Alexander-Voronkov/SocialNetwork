using FluentValidation;

namespace Application.Messages.Commands.UpdateMessage
{
    public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageCommandValidator()
        {
            RuleFor(x => x.Id)
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
