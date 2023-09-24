using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class CreatedReactionEvent : BaseEvent
    {
        public Reaction Event { get; set; }
        public CreatedReactionEvent(Reaction _event)
        {
            this.Event = _event;
        }
    }
}
