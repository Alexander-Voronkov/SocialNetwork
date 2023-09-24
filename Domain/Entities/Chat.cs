using Domain.Common;

namespace Domain.Entities
{
    public class Chat : BaseAuditableEntity<int>
    {
        public int? FirstUserId { get; set; }
        public int? SecondUserId { get; set; }
        public User? FirstUser { get; set; }
        public User? SecondUser { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
