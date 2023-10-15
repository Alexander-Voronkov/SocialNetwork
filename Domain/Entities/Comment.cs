using Domain.Common;

namespace Domain.Entities
{
    public class Comment : BaseAuditableEntity<int>, ISoftDeletable
    {
        public bool IsDeleted { get; set; }
        public int? PostId { get; set; }
        public int? OwnerId { get; set; }
        public int? ReferringCommentId { get; set; }
        public string? CommentBody { get; set; }
        public User? Owner { get; set; }
        public Post? Post { get; set;}
        public Comment? ReferringComment { get; set; }
        public ICollection<Comment>? DependentComments { get; set; }
    }
}
