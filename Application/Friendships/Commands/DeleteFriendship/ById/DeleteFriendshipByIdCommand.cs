using MediatR;

namespace Application.Friendships.Commands.DeleteFriendship.ById
{
    public class DeleteFriendshipByIdCommand : IRequest<int>
    {
        public int? FriendshipId { get; set; }
    }
}
