using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class CreatedPostEvent : BaseEvent
    {
        public CreatedPostEvent(Post Post)
        {
            this.Post = Post;
        }

        public Post Post { get; }
    }
}
