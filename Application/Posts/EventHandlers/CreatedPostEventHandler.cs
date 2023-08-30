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
        public CreatedPostEventHandler(ILogger<CreatedPostEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(CreatedPostEvent notification, CancellationToken cancellationToken)
        {
            // rabbitmq realization
            _logger.LogInformation("New post was created with id: " + notification.PostId);
            return Task.CompletedTask;
        }
    }
}
