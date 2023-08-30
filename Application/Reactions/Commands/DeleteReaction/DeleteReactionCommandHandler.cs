using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Commands.DeleteReaction
{
    public class DeleteReactionCommandHandler : IRequestHandler<DeleteReactionCommand, Unit>
    {
        public Task<Unit> Handle(DeleteReactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
