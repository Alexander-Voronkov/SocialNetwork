using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chats.Commands.DeleteChat
{
    public class DeleteChatCommandHandler : IRequestHandler<DeleteChatCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteChatCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            var deletedChatId = (int)request.ChatId!;

            var chat = await _unitOfWork.ChatsRepository.Get(deletedChatId);

            if (chat == null)
            {
                throw new ChatNotFoundException();
            }

            await _unitOfWork.ChatsRepository.Remove(chat);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return deletedChatId;
        }
    }
}
