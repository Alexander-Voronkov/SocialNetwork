using Enums;

namespace Data.DTOs
{
    public class ReactionDto
    {
        public int? Id { get; set; }
        public ReactionType? Type { get; set; }
        public int? OwnerId { get; set; }
        public int? PostId { get; set; }
        public PostDto? Post { get; set; }
        public UserDto? Owner { get; set; }
    }
}
