using Application.Chats.Queries;
using Application.Users.Queries;

namespace Application.Messages.Queries
{
    public class MessageDto
    {
        public int? Id { get; set; }
        public int? ChatId { get; set; }
        public int? OwnerId { get; set; }
        public string? MessageBody { get; set; }
        public ChatDto? Chat { get; set; }
        public UserDto? Owner { get; set; }
    }
}
