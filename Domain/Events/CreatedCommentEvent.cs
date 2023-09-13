using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events
{
    public class CreatedCommentEvent : BaseEvent
    {
        public CreatedCommentEvent(Comment Comment)
        {
            this.Comment = Comment;
        }

        public Comment Comment { get; }
    }
}
