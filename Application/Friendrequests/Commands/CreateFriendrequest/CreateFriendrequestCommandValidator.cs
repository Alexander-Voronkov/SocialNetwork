using FluentValidation;

namespace Application.Friendrequests.Commands.CreateFriendrequest
{
    public class CreateFriendrequestCommandValidator : AbstractValidator<CreateFriendrequestCommand>
    {
        public CreateFriendrequestCommandValidator() 
        {
            RuleFor(x => x.ToId)
                    .NotNull()
                    .WithMessage("\"To id\" cannot be null");
        }
    }
}
