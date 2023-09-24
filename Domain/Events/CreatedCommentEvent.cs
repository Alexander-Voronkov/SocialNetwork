using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class CreatedCommentEvent : BaseEvent
    {
        public Comment Event { get; set; }
        public CreatedCommentEvent(Comment _event)
        {
            this.Event = _event;
        }
    }
}
