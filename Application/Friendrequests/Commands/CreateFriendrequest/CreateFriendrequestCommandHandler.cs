using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using MediatR;

namespace Application.Friendrequests.Commands.CreateFriendrequest
{
    public class CreateFriendrequestCommandHandler : IRequestHandler<CreateFriendrequestCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUser _user;
        public CreateFriendrequestCommandHandler(IUnitOfWork unitOfWork, IUser user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }
        public async Task<int> Handle(CreateFriendrequestCommand request, CancellationToken cancellationToken)
        {
            var user2 = await _unitOfWork.UsersRepository.Get((int)request.ToId!);
            
            if(user2 == null)
            {
                throw new UserNotFoundException();
            }

            var entity = new Friendrequest()
            {
                FromUserId = _user.Id,
                ToUserId = request.ToId
            };

            entity.AddDomainEvent(new CreatedFriendrequestEvent(entity));

            await _unitOfWork.FriendrequestsRepository.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
