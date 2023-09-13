using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendrequests.Commands.CreateFriendrequest
{
    public class CreateFriendrequestCommand : IRequest<int>
    {
        public int? FromId { get; set; }
        public int? ToId { get; set; }
    }
}
