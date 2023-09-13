using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chats.Queries.GetSingleChat
{
    public class GetSingleChatQuery : IRequest<ChatDto>
    {
        public int? ChatId { get; set; }
    }
}
