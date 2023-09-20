using Application.Users.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chats.Queries
{
    public class ChatDto
    {
        public int? Id { get; set; }
        public int? FirstUserId { get; set; }
        public int? SecondUserId { get; set; }
        public UserDto? FirstUser { get; set; }
        public UserDto? SecondUser { get; set; }
    }
}
