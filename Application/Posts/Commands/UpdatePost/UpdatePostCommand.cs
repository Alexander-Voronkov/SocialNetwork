using MediatR;

namespace Application.Posts.Commands.UpdatePost
{
    public class UpdatePostCommand : IRequest<int>
    {
        public int? PostId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Body { get; set; }
        public IEnumerable<string>? Tags { get; set; }
    }
}
