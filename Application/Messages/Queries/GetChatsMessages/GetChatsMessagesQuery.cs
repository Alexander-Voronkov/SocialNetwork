using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Queries.GetChatsMessages
{
    public class GetChatsMessagesQuery : IRequest<IEnumerable<MessageDto>>
    {
        public int? ChatId { get; set; }
    }
}
