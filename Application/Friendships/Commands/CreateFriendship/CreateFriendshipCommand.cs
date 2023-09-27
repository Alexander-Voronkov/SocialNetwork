using MediatR;

namespace Application.Friendships.Commands.CreateFriendship
{
    public class CreateFriendshipCommand : IRequest<int>
    {
        public int? SecondUserId { get; set; }
    }
}
