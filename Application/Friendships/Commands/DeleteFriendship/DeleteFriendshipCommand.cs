using MediatR;

namespace Application.Friendships.Commands.DeleteFriendship
{
    public class DeleteFriendshipCommand : IRequest<int>
    {
        public int? FriendshipId { get; set; }
    }
}
