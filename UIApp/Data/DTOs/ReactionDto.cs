using Enums;

namespace Data.DTOs
{
    public class ReactionDto
    {
        public ReactionType? Type { get; set; }
        public int? OwnerId { get; set; }
        public int? PostId { get; set; }
    }
}
