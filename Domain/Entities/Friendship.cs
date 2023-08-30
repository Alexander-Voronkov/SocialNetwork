using Domain.Common;

namespace Domain.Entities
{
    public class Friendship : BaseAuditableEntity<int>
    {
        public User? First { get; set; }
        public User? Second { get; set; }
    }
}
