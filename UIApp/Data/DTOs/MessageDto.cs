using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class MessageDto
    {
        public int? ChatId { get; set; }
        public int? OwnerId { get; set; }
        public string? MessageBody { get; set; }
    }
}
