using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Commands.UpdateMessage
{
    public class UpdateMessageCommand : IRequest<int>
    {
        public int? MessageId { get; set; }
        public string? MessageBody { get; set; }
    }
}
