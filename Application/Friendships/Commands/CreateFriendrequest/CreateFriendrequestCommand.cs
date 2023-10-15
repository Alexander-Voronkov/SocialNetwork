using MediatR;

namespace Application.Friendships.Commands.CreateFriendship
{
    public class CreateFriendrequestCommand : IRequest<int>
    {
        public int? SecondUserId { get; set; }
    }
}
