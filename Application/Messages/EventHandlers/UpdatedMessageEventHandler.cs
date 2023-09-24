using Application.Common.Interfaces;
using Domain.Events;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Messages.EventHandlers
{
    public class UpdatedMessageEventHandler : INotificationHandler<UpdatedMessageEvent>
    {
        private readonly IEventBusSender _sender;
        private readonly ILogger<UpdatedMessageEventHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatedMessageEventHandler(IUnitOfWork unitOfWork, IEventBusSender sender, ILogger<UpdatedMessageEventHandler> logger)
        {
            _sender = sender;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdatedMessageEvent notification, CancellationToken cancellationToken)
        {
            var chat = await _unitOfWork.ChatsRepository.FindOne(x => x.Id == (int)notification.Event.ChatId!);
            notification.Event.Chat = chat;
            await _sender.Send(notification);
            _logger.LogInformation("Message was updated with id " + notification.Event.Id + " in chat with id " + notification.Event.ChatId);
        }
    }
}
