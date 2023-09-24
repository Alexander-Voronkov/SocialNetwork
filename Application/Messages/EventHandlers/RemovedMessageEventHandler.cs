using Application.Common.Interfaces;
using Domain.Events;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Messages.EventHandlers
{
    public class RemovedMessageEventHandler : INotificationHandler<RemovedMessageEvent>
    {
        private readonly IEventBusSender _sender;
        private readonly ILogger<RemovedMessageEventHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public RemovedMessageEventHandler(IUnitOfWork unitOfWork, IEventBusSender sender, ILogger<RemovedMessageEventHandler> logger)
        {
            _sender = sender;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemovedMessageEvent notification, CancellationToken cancellationToken)
        {
            var chat = await _unitOfWork.ChatsRepository.FindOne(x => x.Id == (int)notification.Event.ChatId!);
            notification.Event.Chat = chat;
            await _sender.Send(notification);
            _logger.LogInformation("Message was removed with id " + notification.Event.Id + " in chat with id " + notification.Event.ChatId);
        }
    }
}
