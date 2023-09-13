using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messages.EventHandlers
{
    public class CreatedMessageEventHandler : INotificationHandler<CreatedMessageEvent>
    {
        private readonly IEventBusSender _sender;
        private readonly ILogger<CreatedMessageEventHandler> _logger;

        public CreatedMessageEventHandler(IEventBusSender sender, ILogger<CreatedMessageEventHandler> logger)
        {
            _sender = sender;
            _logger = logger;
        }
        public async Task Handle(CreatedMessageEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(notification); 
            _logger.LogInformation("New message was created with id " + notification.Message.Id + " in chat with id " + notification.Message.ChatId);
        }
    }
}
