using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class CreatedChatEvent : BaseEvent
    {
        public Chat Chat { get; }
        public CreatedChatEvent(Chat Chat)
        {
            this.Chat = Chat;
        }
    }
}
