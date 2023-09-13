using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class CreatedFriendrequestEvent : BaseEvent
    {
        public CreatedFriendrequestEvent(Friendrequest Request)
        {
            this.Request = Request;
        }

        public Friendrequest Request { get; }
    }
}
