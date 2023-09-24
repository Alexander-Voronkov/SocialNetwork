using Application.Posts.Queries;
using Application.Users.Queries;
using Domain.Enums;

namespace Application.Reactions.Queries
{
    public class ReactionDto
    {
        public int? Id { get; set; }
        public ReactionType? Type { get; set; }
        public int? PostId { get; set; }
        public int? OwnerId { get; set; }
        public PostDto? Post { get; set; }
        public UserDto? Owner { get; set; }
    }
}
