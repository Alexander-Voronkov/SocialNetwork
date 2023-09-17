

namespace Data.DTOs
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Username { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? PhoneConfirmed { get; set; }
        public ICollection<FriendshipDto>? Friendships { get; set; }
        public ICollection<PostDto>? Posts { get; set; }
        public ICollection<ReactionDto>? Reactions { get; set; }
    }
}
