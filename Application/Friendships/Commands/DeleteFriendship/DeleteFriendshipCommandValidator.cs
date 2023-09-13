using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendships.Commands.DeleteFriendship
{
    public class DeleteFriendshipCommandValidator : AbstractValidator<DeleteFriendshipCommand>
    {
        public DeleteFriendshipCommandValidator()
        {
            RuleFor(x => x.FriendshipId)
                .NotNull()
                .WithMessage("Friendship id cannot be null");
        }   
    }
}
