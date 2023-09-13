using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Queries.GetAllUsersPosts
{
    public class GetAllUsersPostsQuery : IRequest<IEnumerable<PostDto>>
    {
        public int? UserId { get; set; }
    }
}
