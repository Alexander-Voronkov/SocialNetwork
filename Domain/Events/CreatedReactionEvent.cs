using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class CreatedReactionEvent : BaseEvent
    {
        public int Id { get; set; }
    }
}
