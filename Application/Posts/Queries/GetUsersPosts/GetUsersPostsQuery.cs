using Application.Common.Models;
using MediatR;

namespace Application.Posts.Queries.GetUsersPosts
{
    public class GetUsersPostsQuery : IRequest<PaginatedList<PostDto>>
    {
        public int? UserId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
