using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post : BaseAuditableEntity<int>
    {
        public string? PostHeader { get; set; }
        public string? PostBody { get; set; }
        public virtual ICollection<Reaction>? PostReactions { get; set; }
    }
}
