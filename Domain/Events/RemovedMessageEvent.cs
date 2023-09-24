using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class RemovedMessageEvent : BaseEvent
    {
        public Message Event { get; set; }
        public RemovedMessageEvent(Message message)
        {
            Event = message;
        }
    }
}
