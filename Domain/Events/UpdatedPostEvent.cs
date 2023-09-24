using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class UpdatedPostEvent : BaseEvent
    {
        public Post Event { get; set; }
        public UpdatedPostEvent(Post _event)
        {
            Event = _event;
        }
    }
}
