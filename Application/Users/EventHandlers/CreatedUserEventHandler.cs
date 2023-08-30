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
        public CreatedUserEventHandler(ILogger<CreatedUserEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CreatedUserEvent notification, CancellationToken cancellationToken)
        {
            // rabbitmq notification
            _logger.LogInformation("New user was created with id " + notification.UserId);
            return Task.CompletedTask;
        }
    }
}
