using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendrequests.Queries.GetAllFriendrequests
{
    public class GetAllFriendrequestsQuery : IRequest<IEnumerable<FriendrequestDto>>
    {
    }
}
