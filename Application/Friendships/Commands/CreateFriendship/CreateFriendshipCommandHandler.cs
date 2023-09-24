using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using MediatR;

namespace Application.Friendships.Commands.CreateFriendship
{
    public class CreateFriendshipCommandHandler : IRequestHandler<CreateFriendshipCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateFriendshipCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateFriendshipCommand request, CancellationToken cancellationToken)
        {
            var firstUser = await _unitOfWork.UsersRepository.Get((int)request.FirstUserId!);
            var secondUser = await _unitOfWork.UsersRepository.Get((int)request.SecondUserId!);
            
            if(firstUser == null || secondUser == null)
            {
                throw new UserNotFoundException();
            }

            var entity = new Friendship()
            {
                FirstUserId = request.FirstUserId,
                SecondUserId = request.SecondUserId
            };

            entity.AddDomainEvent(new CreatedFriendshipEvent(entity));

            await _unitOfWork.FriendshipsRepository.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
