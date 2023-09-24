using FluentValidation;

namespace Application.Messages.Commands.DeleteMessage
{
    public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageCommandValidator() 
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Message id cannot be null");
        }
    }
}
