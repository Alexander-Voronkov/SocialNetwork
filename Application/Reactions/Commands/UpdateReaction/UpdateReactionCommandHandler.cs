using Application.Common.Exceptions;
using Domain.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        public UpdateReactionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateReactionCommand request, CancellationToken cancellationToken)
        {
            var reaction = await _unitOfWork.ReactionsRepository.Get((int)request.ReactionId!);

            if(reaction == null)
            {
                throw new ReactionNotFoundException();
            }

            reaction.Type = request.Type;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reaction.Id;
        }
    }
}
