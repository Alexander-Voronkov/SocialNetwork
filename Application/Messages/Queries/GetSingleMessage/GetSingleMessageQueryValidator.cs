using FluentValidation;

namespace Application.Messages.Queries.GetSingleMessage
{
    public class GetSingleMessageQueryValidator : AbstractValidator<GetSingleMessageQuery>
    {
        public GetSingleMessageQueryValidator() 
        {
            RuleFor(x => x.MessageId)
                .NotNull()
                .WithMessage("Message id cannot be null");
        }
    }
}
