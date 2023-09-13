using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class CreatedUserEvent : BaseEvent
    {
        public User User { get; }
        public CreatedUserEvent(User User)
        {
            this.User = User;
        }
    }
}
