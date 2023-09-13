using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendships.Queries.GetAllUsersFriendships
{
    public class GetAllUsersFriendshipsQuery : IRequest<IEnumerable<FriendshipDto>>
    {
        public int? UserId { get; set; }
    }
}
