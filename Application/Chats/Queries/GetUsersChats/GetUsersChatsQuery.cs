using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chats.Queries.GetUsersChats
{
    public class GetUsersChatsQuery : IRequest<IEnumerable<ChatDto>>
    {
        public int? UserId { get; set; }
    }
}
