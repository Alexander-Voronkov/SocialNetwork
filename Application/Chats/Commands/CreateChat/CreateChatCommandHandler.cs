using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chats.Commands.CreateChat
{
    public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateChatCommandHandler(IUnitOfWork unit) 
        {
            _unitOfWork = unit;
        }
        public async Task<int> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var entity = new Chat
            {
                FirstUserId = request.FirstUserId,
                SecondUserId = request.SecondUserId,
            };

            entity.AddDomainEvent(new CreatedChatEvent(entity));

            await _unitOfWork.ChatsRepository.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
