using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class CreatedReactionEvent : BaseEvent
    {
        public Reaction Reaction { get; }
        public CreatedReactionEvent(Reaction Reaction)
        {
            this.Reaction = Reaction;
        }
    }
}
