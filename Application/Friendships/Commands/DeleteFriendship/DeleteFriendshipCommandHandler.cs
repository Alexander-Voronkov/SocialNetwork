using Application.Common.Exceptions;
using Application.Friendrequests.Commands.DeleteFriendrequest;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendships.Commands.DeleteFriendship
{
    public class DeleteFriendshipCommandHandler : IRequestHandler<DeleteFriendshipCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteFriendshipCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteFriendshipCommand request, CancellationToken cancellationToken)
        {
            var friendship = await _unitOfWork.FriendshipsRepository.Get((int)request.FriendshipId!);
            
            if(friendship == null)
            {
                throw new FriendshipNotFoundException();
            }

            await _unitOfWork.FriendshipsRepository.Remove(friendship);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return friendship.Id;
        }
    }
}
