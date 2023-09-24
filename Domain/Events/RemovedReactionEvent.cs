using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class RemovedReactionEvent : BaseEvent
    {
        public Reaction Event { get; set; }
        public RemovedReactionEvent(Reaction reaction)
        {
            Event = reaction;
        }
    }
}
