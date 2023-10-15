using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class CreatedFriendrequestEvent : BaseEvent
    {
        public Friendship Event { get; set; }
        public CreatedFriendrequestEvent(Friendship _event)
        {
            this.Event = _event;
        }
    }
}
