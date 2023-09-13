using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendships.Commands.DeleteFriendship
{
    public class DeleteFriendshipCommand : IRequest<int>
    {
        public int? FriendshipId { get; set; }
    }
}
