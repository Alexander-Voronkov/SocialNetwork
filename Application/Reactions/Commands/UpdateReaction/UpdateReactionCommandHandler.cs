using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Commands.UpdateReaction
{
    public class UpdateReactionCommandHandler : IRequestHandler<UpdateReactionCommand, int>
    {
        public Task<int> Handle(UpdateReactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
