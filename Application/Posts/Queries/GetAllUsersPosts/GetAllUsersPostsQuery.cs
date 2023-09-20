using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Queries.GetAllUsersPosts
{
    public class GetAllUsersPostsQuery : IRequest<PaginatedList<PostDto>>
    {
        public int? UserId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
