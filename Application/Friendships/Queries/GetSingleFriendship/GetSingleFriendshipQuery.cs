using MediatR;

namespace Application.Friendships.Queries.GetSingleFriendship
{
    public class GetSingleFriendshipQuery : IRequest<FriendshipDto>
    {
        public int? FriendshipId { get; set; }
    }
}
