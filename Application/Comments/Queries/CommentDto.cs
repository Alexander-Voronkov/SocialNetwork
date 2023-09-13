using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Queries
{
    public class CommentDto
    {
        public int? PostId { get; set; }
        public int? OwnerId { get; set; }
        public int? ReferringCommentId { get; set; }
        public string? CommentBody { get; set; }
    }
}
