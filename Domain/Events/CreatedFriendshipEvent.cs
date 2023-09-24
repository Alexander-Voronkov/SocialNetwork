using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class CreatedFriendshipEvent : BaseEvent
    {
        public Friendship Event { get; set; }
        public CreatedFriendshipEvent(Friendship _event)
        {
            this.Event = _event;
        }
    }
}
