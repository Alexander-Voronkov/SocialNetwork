using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class CreatedPostEvent : BaseEvent
    {
        public int PostId { get; set; }
    }
}
