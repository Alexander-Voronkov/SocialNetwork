using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reaction : BaseAuditableEntity<int>
    {
        public ReactionType Type { get; set; }
        public int? OwnerId { get; set; }
        public int? PostId { get; set; }
        public Post? Post { get; set; }
        public User? Owner { get; set; }
    }
}
