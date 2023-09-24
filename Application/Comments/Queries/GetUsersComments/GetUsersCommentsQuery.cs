using Application.Common.Models;
using MediatR;

namespace Application.Comments.Queries.GetUsersComments
{
    public class GetUsersCommentsQuery : IRequest<PaginatedList<CommentDto>>
    {
        public int? UserId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
