using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendrequests.Commands.DeleteFriendrequest
{
    public class DeleteFriendrequestCommand : IRequest<int>
    {
        public int? FriendrequestId { get; set; }
    }
}
