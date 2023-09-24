using MediatR;

namespace Application.Friendrequests.Commands.DeleteFriendrequest
{
    public class DeleteFriendrequestCommand : IRequest<int>
    {
        public int? FriendrequestId { get; set; }
    }
}
