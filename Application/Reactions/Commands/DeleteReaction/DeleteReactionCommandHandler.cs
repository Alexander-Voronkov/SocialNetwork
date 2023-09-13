using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Commands.DeleteReaction
{
    public class DeleteReactionCommandHandler : IRequestHandler<DeleteReactionCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteReactionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteReactionCommand request, CancellationToken cancellationToken)
        {
            var reaction = await _unitOfWork.ReactionsRepository.Get((int)request.ReactionId!);

            if (reaction == null)
            {
                throw new ReactionNotFoundException();
            }

            await _unitOfWork.ReactionsRepository.Remove(reaction);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reaction.Id;
        }
    }
}
