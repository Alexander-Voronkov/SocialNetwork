using Application.Common.Interfaces;
using Domain.Events;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Messages.EventHandlers
{
    public class CreatedMessageEventHandler : INotificationHandler<CreatedMessageEvent>
    {
        private readonly IEventBusSender _sender;
        private readonly ILogger<CreatedMessageEventHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreatedMessageEventHandler(IUnitOfWork unitOfWork, IEventBusSender sender, ILogger<CreatedMessageEventHandler> logger)
        {
            _sender = sender;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(CreatedMessageEvent notification, CancellationToken cancellationToken)
        {
            var chat = await _unitOfWork.ChatsRepository.FindOne(x => x.Id == (int)notification.Event.ChatId!);
            notification.Event.Chat = chat;
            await _sender.Send(notification); 
            _logger.LogInformation("New message was created with id " + notification.Event.Id + " in chat with id " + notification.Event.ChatId);
        }
    }
}
