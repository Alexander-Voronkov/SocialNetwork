using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chats.EventHandlers
{
    public class CreatedChatEventHandler : INotificationHandler<CreatedChatEvent>
    {
        private readonly IEventBusSender _sender;
        private readonly ILogger<CreatedChatEventHandler> _logger;
        public CreatedChatEventHandler(IEventBusSender sender, ILogger<CreatedChatEventHandler> logger)
        {
            _sender = sender;
            _logger = logger;
        }
        public async Task Handle(CreatedChatEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(notification);
            _logger.LogInformation(
                "New chat was created with id "+
                notification.Chat.Id + 
                " between user " + 
                notification.Chat.FirstUserId + 
                " and user " + 
                notification.Chat.SecondUserId);
        }
    }
}
