using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chats.Commands.CreateChat
{
    public class CreateChatCommand : IRequest<int>
    {
        public int? FirstUserId { get; set; }
        public int? SecondUserId { get; set; }
    }
}
