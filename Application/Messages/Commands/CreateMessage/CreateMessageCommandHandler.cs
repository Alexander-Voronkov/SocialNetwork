using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using MediatR;

namespace Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUser _user;
        public CreateMessageCommandHandler(IUnitOfWork unitOfWork, IUser user)
        {
            _unitOfWork = unitOfWork;
            _user = user;
        }

        public async Task<int> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var chat = await _unitOfWork.ChatsRepository.Get((int)request.ChatId!);
            
            if(chat == null)
            {
                throw new ChatNotFoundException();
            }

            if(chat.FirstUserId != _user.Id && chat.SecondUserId != _user.Id)
            {
                throw new NotAllowedToChatException();
            }

            var entity = new Message()
            {
                ChatId = request.ChatId,
                OwnerId = _user.Id,
                MessageBody = request.MessageBody,
            };

            await _unitOfWork.MessagesRepository.Add(entity);

            entity.AddDomainEvent(new CreatedMessageEvent(entity));
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
