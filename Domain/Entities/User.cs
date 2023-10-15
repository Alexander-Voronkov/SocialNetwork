using Domain.Common;

namespace Domain.Entities
{
    public class User : BaseAuditableEntity<int>, ISoftDeletable
    {
        public bool IsDeleted { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? PhoneConfirmed { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Friendship>? Friendships { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public ICollection<Chat>? Chats { get; set; }
        public ICollection<Reaction>? Reactions { get; set; }
    }
}
