using MediatR;

namespace Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest<int>
    {
        public int? Id { get; set; }
        public string? CommentBody { get; set; }
    }
}
