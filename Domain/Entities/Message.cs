using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Message : BaseAuditableEntity<int>
    {
        public int? ChatId { get; set; }
        public int? OwnerId { get; set; }
        public string? MessageBody { get; set; }
        public Chat? Chat { get; set; }
        public User? Owner { get; set; }
    }
}
