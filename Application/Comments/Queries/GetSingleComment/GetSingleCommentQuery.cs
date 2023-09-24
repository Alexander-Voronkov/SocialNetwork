using MediatR;

namespace Application.Comments.Queries.GetSingleComment
{
    public class GetSingleCommentQuery : IRequest<CommentDto>
    {
        public int? CommentId { get; set; }
    }
}
