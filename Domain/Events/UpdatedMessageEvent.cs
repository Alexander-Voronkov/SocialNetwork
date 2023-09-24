using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class UpdatedMessageEvent : BaseEvent
    {
        public Message Event { get; set; }
        public UpdatedMessageEvent(Message _event)
        {
            Event = _event;
        }
    }
}
