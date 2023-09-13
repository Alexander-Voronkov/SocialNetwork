using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendrequests.Queries.GetAllUsersFriendrequests.Received
{
    public class GetAllUsersReceivedFriendrequestsQuery : IRequest<IEnumerable<FriendrequestDto>>
    {
        public int? UserId { get; set; }
    }
}
