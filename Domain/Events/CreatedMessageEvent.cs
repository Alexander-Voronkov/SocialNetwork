using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class CreatedMessageEvent : BaseEvent
    {
        public Message Message { get; set; }
        public CreatedMessageEvent(Message Message) 
        {
            this.Message = Message;
        }
    }
}
