using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
