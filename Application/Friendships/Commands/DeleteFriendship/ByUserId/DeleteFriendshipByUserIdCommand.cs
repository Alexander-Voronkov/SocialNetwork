using MediatR;

namespace Application.Friendships.Commands.DeleteFriendship.ByUserId
{
    public class DeleteFriendshipByUserIdCommand : IRequest<int>
    {
        public int? UserId { get; set; }
    }
}
