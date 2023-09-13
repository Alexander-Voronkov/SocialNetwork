using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class BaseAuditableEntity<PKeyType> : BaseEntity<PKeyType>
    {
        public DateTimeOffset CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset LastModifiedAt { get; set; }
        public int? LastModifiedBy { get; set; }
    }
}
