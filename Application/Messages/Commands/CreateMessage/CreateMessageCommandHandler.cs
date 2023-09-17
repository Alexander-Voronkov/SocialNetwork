using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateMessageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UsersRepository.Get((int)request.OwnerId!);
            
            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var chat = await _unitOfWork.ChatsRepository.Get((int)request.ChatId!);
            
            if(chat == null)
            {
                throw new ChatNotFoundException();
            }

            var entity = new Message()
            {
                ChatId = request.ChatId,
                OwnerId = request.OwnerId,
                MessageBody = request.MessageBody,
            };

            entity.AddDomainEvent(new CreatedMessageEvent(entity));

            await _unitOfWork.MessagesRepository.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
