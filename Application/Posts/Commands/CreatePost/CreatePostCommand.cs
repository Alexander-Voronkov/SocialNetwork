using MediatR;

namespace Application.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<int>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Body { get; set; }
        public IEnumerable<string>? Tags { get; set; }
    }
}
