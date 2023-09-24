using MediatR;

namespace Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<int>
    {
        public int? Id { get; set; }
    }
}
