using MediatR;

namespace Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<int>
    {
        public int? PostId { get; set; }
        public int? ReferringCommentId { get; set; }
        public string? CommentBody { get; set; }
    }
}
