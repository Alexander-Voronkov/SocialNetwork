using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Queries.GetAllUsersMessages
{
    public class GetAllUsersMessagesQuery : IRequest<IEnumerable<MessageDto>>
    {
        public int? UserId { get; set; }
    }
}
