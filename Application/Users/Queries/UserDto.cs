using Application.Chats.Queries;
using Application.Comments.Queries;
using Application.Friendrequests.Queries;
using Application.Friendships.Queries;
using Application.Messages.Queries;
using Application.Posts.Queries;
using Application.Reactions.Queries;

namespace Application.Users.Queries
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
        public IEnumerable<FriendrequestDto>? Friendrequests { get; set; }
        public IEnumerable<CommentDto>? Comments { get; set; }
        public IEnumerable<ChatDto>? Chats { get; set; }
    }
}
