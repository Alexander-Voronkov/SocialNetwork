using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Queries
{
    public class MessageDto
    {
        public int? ChatId { get; set; }
        public int? OwnerId { get; set; }
        public string? MessageBody { get; set; }
    }
}
