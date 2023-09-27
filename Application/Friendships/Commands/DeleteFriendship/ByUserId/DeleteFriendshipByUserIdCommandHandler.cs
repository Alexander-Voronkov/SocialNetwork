using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Friendships.Commands.DeleteFriendship.ByUserId
{
    public class DeleteFriendshipByUserIdCommandHandler : IRequestHandler<DeleteFriendshipByUserIdCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUser _user;
        public DeleteFriendshipByUserIdCommandHandler(IUnitOfWork unitOfWork, IUser user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }
        public async Task<int> Handle(DeleteFriendshipByUserIdCommand request, CancellationToken cancellationToken)
        {
            var friendship = await _unitOfWork.FriendshipsRepository.FindOne(x=>
                (x.FirstUserId == request.UserId && x.SecondUserId == _user.Id) ||
                (x.FirstUserId == _user.Id && x.SecondUserId == request.UserId));

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
