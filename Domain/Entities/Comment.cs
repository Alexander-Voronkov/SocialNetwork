using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment : BaseAuditableEntity<int>
    {
        public int? PostId { get; set; }
        public int? OwnerId { get; set; }
        public int? ReferringCommentId { get; set; }
        public string? CommentBody { get; set; }
        public User? Owner { get; set; }
        public Post? Post { get; set;}
        public Comment? ReferringComment { get; set; }
    }
}
