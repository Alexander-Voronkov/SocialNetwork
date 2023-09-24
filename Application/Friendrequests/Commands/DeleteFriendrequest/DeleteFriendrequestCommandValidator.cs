using FluentValidation;

namespace Application.Friendrequests.Commands.DeleteFriendrequest
{
    public class DeleteFriendrequestCommandValidator : AbstractValidator<DeleteFriendrequestCommand>
    {
        public DeleteFriendrequestCommandValidator()
        {
            RuleFor(x => x.FriendrequestId)
                .NotNull()
                .WithMessage("Friendrequest id cannot be null");
        }
    }
}
