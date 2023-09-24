using Application.Common.Exceptions;
using Domain.Events;
using Domain.Interfaces;
using MediatR;

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
            var message = await _unitOfWork.MessagesRepository.Get((int)request.Id!);
        
            if(message == null)
            {
                throw new MessageNotFoundException();
            }

            await _unitOfWork.MessagesRepository.Remove(message);

            message.AddDomainEvent(new RemovedMessageEvent(message));
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return message.Id;
        }
    }
}
