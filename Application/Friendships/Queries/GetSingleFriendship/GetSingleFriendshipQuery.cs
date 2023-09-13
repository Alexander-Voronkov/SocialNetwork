using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendships.Queries.GetSingleFriendship
{
    public class GetSingleFriendshipQuery : IRequest<FriendshipDto>
    {
        public int? FriendshipId { get; set; }
    }
}
