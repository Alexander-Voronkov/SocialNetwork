using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chats.Commands.DeleteChat
{
    public class DeleteChatCommand : IRequest<int>
    {
        public int? ChatId { get; set; }
    }
}
