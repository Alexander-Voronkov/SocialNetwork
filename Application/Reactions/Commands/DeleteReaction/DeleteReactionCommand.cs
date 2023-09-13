using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Commands.DeleteReaction
{
    public class DeleteReactionCommand : IRequest<int>
    {
        public int? ReactionId { get; set; }
    }

}
