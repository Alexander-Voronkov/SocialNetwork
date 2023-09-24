using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Chats.Commands.CreateChat
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUser _user;
        public CreateChatCommandHandler(IUser user, IUnitOfWork unit, IMapper mapper) 
        {
            _unitOfWork = unit;
            _mapper = mapper;
            _user = user;
        }
        public async Task<int> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var chat = await _unitOfWork.ChatsRepository.FindOne(x =>
                (x.FirstUserId == _user.Id && x.SecondUserId == request.SecondUserId) ||
                (x.SecondUserId == _user.Id && x.FirstUserId == request.SecondUserId));

            if(chat != null)
            {
                throw new ChatAlreadyExistsException();
            }

            var friendship = await _unitOfWork.FriendshipsRepository.FindOne(x =>
                 (x.FirstUserId == _user.Id && x.SecondUserId == request.SecondUserId) ||
                 (x.SecondUserId == _user.Id && x.FirstUserId == request.SecondUserId));

            if(friendship == null)
            {
                throw new NotFriendsException();
            }

            var entity = new Chat
            {
                FirstUserId = _user.Id,
                SecondUserId = request.SecondUserId,
            };

            await _unitOfWork.ChatsRepository.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
