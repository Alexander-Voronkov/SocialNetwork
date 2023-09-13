using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class CreatedFriendshipEvent : BaseEvent
    { 
        public Friendship Friendship { get; }
        public CreatedFriendshipEvent(Friendship Friendship)
        {
            this.Friendship = Friendship;
        }
    }
}
