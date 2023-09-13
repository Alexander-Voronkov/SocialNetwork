using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.EventHandlers
{
    public class CreatedUserEventHandler : INotificationHandler<CreatedUserEvent>
    {
        private readonly ILogger<CreatedUserEventHandler> _logger;
        private readonly IEventBusSender _sender;
        public CreatedUserEventHandler(ILogger<CreatedUserEventHandler> logger, IEventBusSender eventBus)
        {
            _logger = logger;
            _sender = eventBus;
        }

        public async Task Handle(CreatedUserEvent notification, CancellationToken cancellationToken)
        {
            await _sender.Send(notification);
            _logger.LogInformation("New user was created with id " + notification.User.Id);
        }
    }
}
