using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseAuditableEntity<int>
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? PhoneConfirmed { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Friendrequest>? Friendrequests { get; set; }
        public ICollection<Friendship>? Friendships { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public ICollection<Chat>? Chats { get; set; }
        public ICollection<Reaction>? Reactions { get; set; }
    }
}
