using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendrequests.Commands.CreateFriendrequest
{
    public class CreateFriendrequestCommandValidator : AbstractValidator<CreateFriendrequestCommand>
    {
        public CreateFriendrequestCommandValidator() 
        {
            RuleFor(x => x.ToId)
                    .NotNull()
                    .WithMessage("\"To id\" cannot be null");

            RuleFor(x => x.FromId)
                .NotNull()
                .WithMessage("\"From id\" cannot be null")
                .NotEqual(x=>x.ToId)
                .WithMessage("Id's cannot equal to each other");
        }
    }
}
