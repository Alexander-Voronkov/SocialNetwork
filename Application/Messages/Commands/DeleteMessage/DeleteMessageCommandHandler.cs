using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Commands.DeleteMessage
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteMessageCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.MessagesRepository.Get((int)request.MessageId!);
        
            if(message == null)
            {
                throw new MessageNotFoundException();
            }

            await _unitOfWork.MessagesRepository.Remove(message);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return message.Id;
        }
    }
}
