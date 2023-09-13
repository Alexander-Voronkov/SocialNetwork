using Application.Common.Interfaces;
using Domain.Common;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.EventHandlers
{
    public class CreatedPostEventHandler : INotificationHandler<CreatedPostEvent>
    {
        private readonly ILogger<CreatedPostEventHandler> _logger;
        private readonly IEventBusSender _sender;
        public CreatedPostEventHandler(ILogger<CreatedPostEventHandler> logger, IEventBusSender eventBus)
        {
            _logger = logger;
            _sender = eventBus;
        }
        public async Task Handle(CreatedPostEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(notification);
            _logger.LogInformation("New post was created with id: " + notification.Post.Id);
        }
    }
}
