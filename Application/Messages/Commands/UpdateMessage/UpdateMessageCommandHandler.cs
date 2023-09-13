using Application.Common.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.Commands.UpdateMessage
{
    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateMessageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _unitOfWork.MessagesRepository.Get((int)request.MessageId!);

            if (message == null)
            {
                throw new MessageNotFoundException();
            }

            message.MessageBody = request.MessageBody;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return message.Id;
        }
    }
}
