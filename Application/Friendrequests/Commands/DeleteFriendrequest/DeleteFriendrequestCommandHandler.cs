using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Friendrequests.Commands.DeleteFriendrequest
{
    public class DeleteFriendrequestCommandHandler : IRequestHandler<DeleteFriendrequestCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteFriendrequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteFriendrequestCommand request, CancellationToken cancellationToken)
        {
            var friendRequest = await _unitOfWork.FriendrequestsRepository.Get((int)request.FriendrequestId!);
            
            if(friendRequest == null)
            {
                throw new FriendrequestNotFoundException();
            }

            await _unitOfWork.FriendrequestsRepository.Remove(friendRequest);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return friendRequest.Id;
        }
    }
}
