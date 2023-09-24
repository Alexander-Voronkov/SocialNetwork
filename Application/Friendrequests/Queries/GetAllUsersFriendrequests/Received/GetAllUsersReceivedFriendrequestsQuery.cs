using Application.Common.Models;
using MediatR;

namespace Application.Friendrequests.Queries.GetAllUsersFriendrequests.Received
{
    public class GetAllUsersReceivedFriendrequestsQuery : IRequest<PaginatedList<FriendrequestDto>>
    {
        public int? UserId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
