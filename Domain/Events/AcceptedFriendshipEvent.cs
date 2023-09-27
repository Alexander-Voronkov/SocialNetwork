using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class AcceptedFriendshipEvent : BaseEvent
    {
        public Friendship Event { get; set; }
        public AcceptedFriendshipEvent(Friendship _event)
        {
            Event = _event;
        }
    }
}
