using Application.Common.Models;
using MediatR;

namespace Application.Friendships.Queries.GetUsersFriendrequests.Received
{
    public class GetUsersReceivedFriendrequestsQuery : IRequest<PaginatedList<FriendshipDto>>
    {
        public int? UserId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
