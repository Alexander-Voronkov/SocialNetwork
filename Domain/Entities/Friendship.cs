using Domain.Common;

namespace Domain.Entities
{
    public class Friendship : BaseAuditableEntity<int>
    {
        public int? FirstUserId { get; set; }
        public int? SecondUserId { get; set; }
        public bool IsAccepted { get; set; }
        public User? FirstUser { get; set; }
        public User? SecondUser { get; set; }
    }
}
