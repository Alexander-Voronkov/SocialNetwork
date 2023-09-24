using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class CreatedMessageEvent : BaseEvent
    {
        public Message Event { get; set; }
        public CreatedMessageEvent(Message _event)
        {
            this.Event = _event;
        }
    }
}
