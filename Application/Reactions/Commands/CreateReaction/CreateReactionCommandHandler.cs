using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using Domain.Interfaces;
using MediatR;

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

            var reaction = await _unitOfWork.ReactionsRepository.FindOne(x=>
                    x.PostId == request.PostId && 
                    _user.Id == x.OwnerId && 
                    x.Type == request.Type);

            if (reaction != null)
            {
                await _unitOfWork.ReactionsRepository.Remove(reaction);
                reaction.AddDomainEvent(new RemovedReactionEvent(reaction));
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return reaction.Id;
            }

            reaction = await _unitOfWork.ReactionsRepository.FindOne(x =>
                x.OwnerId == _user.Id &&
                x.PostId == request.PostId);

            if(reaction != null)
            {
                await _unitOfWork.ReactionsRepository.Remove(reaction);
                reaction.AddDomainEvent(new RemovedReactionEvent(reaction));
            }

            reaction = new Reaction
            {
                PostId = request.PostId,
                OwnerId = _user.Id,
                Type = (ReactionType)request.Type!                
            };

            reaction.AddDomainEvent(new CreatedReactionEvent(reaction));

            await _unitOfWork.ReactionsRepository.Add(reaction);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return reaction.Id;
        }
    }
}
