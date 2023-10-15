using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using MediatR;

namespace Application.Friendships.Commands.CreateFriendship
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
            var friendship = await _unitOfWork.FriendshipsRepository.FindOne(x=>
                ((x.SecondUserId == (int)request.SecondUserId! && x.FirstUserId == _user.Id) ||
                (x.FirstUserId == (int)request.SecondUserId! && x.SecondUserId == _user.Id)));

            if(friendship != null)
            {
                throw new FriendrequestAlreadyExistsException();
            }

            var entity = new Friendship()
            {
                FirstUserId = _user.Id,
                SecondUserId = request.SecondUserId,
            };

            await _unitOfWork.FriendshipsRepository.Add(entity);

            entity.AddDomainEvent(new CreatedFriendrequestEvent(entity));
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
