using Domain.Common;

namespace Domain.Entities
{
    public class Post : BaseAuditableEntity<int>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Body { get; set; }
        public int? OwnerId { get; set; }
        public User? Owner { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Reaction>? Reactions { get; set; }
        public IEnumerable<string>? Tags { get; set; }
    }
}
