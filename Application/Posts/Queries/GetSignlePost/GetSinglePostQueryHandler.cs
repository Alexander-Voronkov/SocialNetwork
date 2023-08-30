using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Queries.GetSignlePost
{
    public class GetSinglePostQueryHandler : IRequestHandler<GetSinglePostQuery, PostDto>
    {
        public Task<PostDto> Handle(GetSinglePostQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
