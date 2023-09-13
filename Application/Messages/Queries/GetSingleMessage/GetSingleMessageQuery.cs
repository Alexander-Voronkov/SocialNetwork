using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Queries.GetSingleMessage
{
    public class GetSingleMessageQuery : IRequest<MessageDto>
    {
        public int? MessageId { get; set; }
    }
}
