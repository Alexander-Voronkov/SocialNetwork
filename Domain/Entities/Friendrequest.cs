using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Friendrequest : BaseAuditableEntity<int>
    {
        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }
        public User? From { get; set; }
        public User? To { get; set; }
    }
}
