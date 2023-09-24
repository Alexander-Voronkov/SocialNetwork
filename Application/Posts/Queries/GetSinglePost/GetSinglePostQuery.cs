using MediatR;

namespace Application.Posts.Queries.GetSinglePost
{
    public class GetSinglePostQuery : IRequest<PostDto>
    {
        public int? PostId { get; set; }
    }
}
