using MediatR;

namespace Application.Friendrequests.Commands.CreateFriendrequest
{
    public class CreateFriendrequestCommand : IRequest<int>
    {
        public int? ToId { get; set; }
    }
}
