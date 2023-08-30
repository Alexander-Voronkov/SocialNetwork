using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reactions.EventHandlers
{
    public class CreatedReactionEventHandler : INotificationHandler<CreatedReactionEvent>
    {
        private readonly ILogger _logger;
        public CreatedReactionEventHandler(ILogger<CreatedReactionEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(CreatedReactionEvent notification, CancellationToken cancellationToken)
        {
            // rabbitmq notification

            _logger.LogInformation("A new reaction was created with id " + notification.Id);
            return Task.CompletedTask;
        }
    }
}
