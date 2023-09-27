using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Friendships.Commands.DeleteFriendship.ById
{
    public class DeleteFriendshipByIdCommandHandler : IRequestHandler<DeleteFriendshipByIdCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteFriendshipByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteFriendshipByIdCommand request, CancellationToken cancellationToken)
        {
            var friendship = await _unitOfWork.FriendshipsRepository.Get((int)request.FriendshipId!);

            if (friendship == null)
            {
                throw new FriendshipNotFoundException();
            }

            await _unitOfWork.FriendshipsRepository.Remove(friendship);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return friendship.Id;
        }
    }
}
