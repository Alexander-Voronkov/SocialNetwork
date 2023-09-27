using MediatR;

namespace Application.Friendships.Commands.AcceptFriendship
{
    public class AcceptFriendshipCommand : IRequest<int>
    {
        public int? FriendshipId { get; set; }
    }
}
