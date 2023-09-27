using Application.Common.Exceptions;
using Domain.Events;
using Domain.Interfaces;
using MediatR;

namespace Application.Friendships.Commands.AcceptFriendship
{
    public class AcceptFriendshipCommandHandler : IRequestHandler<AcceptFriendshipCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AcceptFriendshipCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(AcceptFriendshipCommand request, CancellationToken cancellationToken)
        {
            var friendship = await _unitOfWork.FriendshipsRepository.Get((int)request.FriendshipId!);

            if (friendship == null) 
            {
                throw new FriendshipNotFoundException();
            }

            friendship.IsAccepted = true;

            friendship.AddDomainEvent(new AcceptedFriendshipEvent(friendship));

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return friendship.Id;
        }
    }
}
