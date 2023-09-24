using MediatR;

namespace Application.Posts.Commands.DeletePost
{
    public class DeletePostCommand : IRequest<int>
    {
        public int? PostId { get; set; }
    }
}
