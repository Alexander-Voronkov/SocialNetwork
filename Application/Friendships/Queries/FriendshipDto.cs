using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendships.Queries
{
    public class FriendshipDto
    {
        public int? FirstUserId { get; set; }
        public int? SecondUserId { get; set; }
    }
}
