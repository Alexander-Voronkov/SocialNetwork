

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
        public IEnumerable<FriendshipDto>? Friendships { get; set; }
        public IEnumerable<PostDto>? Posts { get; set; }
        public IEnumerable<ReactionDto>? Reactions { get; set; }
        public IEnumerable<MessageDto>? Messages { get; set; }
        public IEnumerable<CommentDto>? Comments { get; set; }
        public IEnumerable<ChatDto>? Chats { get; set; }
    }
}
