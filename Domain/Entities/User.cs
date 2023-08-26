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
        public string? Email { get; set; }
        public ICollection<Friendship>? Friendships { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Reaction>? Reactions { get; set; }
    }
}
