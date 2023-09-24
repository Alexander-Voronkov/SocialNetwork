using Application.Common.Exceptions;
using Domain.Events;
using Domain.Interfaces;
using MediatR;

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
            var message = await _unitOfWork.MessagesRepository.Get((int)request.Id!);

            if (message == null)
            {
                throw new MessageNotFoundException();
            }

            message.MessageBody = request.MessageBody;

            message.AddDomainEvent(new UpdatedMessageEvent(message));

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return message.Id;
        }
    }
}
