using FluentValidation;

namespace Application.Friendships.Commands.CreateFriendship
{
    public class CreateFriendrequestCommandValidator : AbstractValidator<CreateFriendrequestCommand>
    {
        public CreateFriendrequestCommandValidator() 
        {
            RuleFor(x => x.SecondUserId)
                .NotNull()
                .WithMessage("Future friend id cannot be null");
        }
    }
}
