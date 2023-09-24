using Application.Common.Models;
using MediatR;

namespace Application.Comments.Queries.GetPostComments
{
    public class GetPostCommentsQuery : IRequest<PaginatedList<CommentDto>>
    {
        public int? PostId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
