using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class CreatedFriendrequestEvent : BaseEvent
    {
        public Friendrequest Event { get; set; }
        public CreatedFriendrequestEvent(Friendrequest _event)
        {
            this.Event = _event;
        }
    }
}
