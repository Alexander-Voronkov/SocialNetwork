using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.Commands.CreateReaction
{
    public class CreateReactionCommandHandler : IRequestHandler<CreateReactionCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUser _user;
        public CreateReactionCommandHandler(IUnitOfWork unitOfWork, IUser user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }
        public async Task<int> Handle(CreateReactionCommand request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostsRepository.Get((int)request.PostId!);

            if(post == null)
            {
                throw new PostNotFoundException();
            }

            var entity = new Reaction
            {
                PostId = request.PostId,
                OwnerId = request.OwnerId,
                Type = (ReactionType)request.Type!                
            };

            entity.AddDomainEvent(new CreatedReactionEvent(entity));

            await _unitOfWork.ReactionsRepository.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
